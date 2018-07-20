﻿using Common;
using EFModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers.Gome
{
    public class GomeGameController : BaseController
    {
        private const string enter_url = "http://www.chinazhihuiping.com/wxredpackets/GomeGame/Activity";
        private const string wx_url = "http://www.chinazhihuiping.com/wxredpackets/GomeGame/WXActivity";
        private const string money_url = "http://www.chinazhihuiping.com/wxredpackets/GomeGame/GiveMoney";
        private const string str_a_activityname = "【国美互动游戏】";
        private const string str_b_activityname = "【国美互动游戏】发生重定向-现金红包跳转";
        private const string str_c_activityname = "【国美互动游戏】发生重定向-实物红包跳转";
        private const string str_d_activityname = "【国美互动游戏】-实物已领取";
        private const string str_e_activityname = "【国美互动游戏】-取奖品awardsModel.Class == \"\"";
        private const string str_f_activityname = "【国美互动游戏】-取奖品awardsModel.Class == null";

        public Dictionary<string, string> awards_code = new Dictionary<string, string>(){ 
                        {"多肉","A"},
                        {"卡通扇子笔","B"},  
                        {"卡通卡套","C"},
                        {"卡通记事本","D"},
                        {"开瓶器","E"},
                        {"大嘴猴卡套","F"},

                         //增加
                        {"购物车","G"},
                        {"扇子笔","H"},
                        {"卡套","I"},
                        {"开瓶器1","J"},
                        {"电动风扇","K"},
                        {"记事本","L"},
                        {"卡通水杯","M"}
        };

        /// <summary>
        /// 活动入口
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="awardid"></param>
        /// <param name="awardname"></param>
        /// <returns></returns>
        public ActionResult Activity(string activityId, string awardid, string awardname)
        {
            #region 扫码计数

            scanCountDi.getBll().CountByNameAndId(activityId, str_a_activityname);

            #endregion

            ActionResult empty = new EmptyResult();

            #region 前端网络异常


            if (string.IsNullOrWhiteSpace(awardid))
            {
                scanCountDi.getBll().CountByNameAndId(activityId, string.Format("【国美互动游戏】-flash端网络异常-【{0}】", awardname));
                return View("Nogift");
            }

            #endregion

            try
            {
                #region 记录日志

                //日志记录公共部分          
                TRP_ClientLog entity = new TRP_ClientLog();
                entity.CreateTime = DateTime.Now;
                entity.DeleteMark = false;
                entity.Enable = true;
                entity.PageUrl = HttpContext.Request.Url.AbsoluteUri ?? enter_url;
                entity.IPAddress = HttpContext.Request.UserHostAddress ?? "127.0.0.1";
                entity.ActivityId = Convert.ToInt32(activityId);

                #endregion

                #region 重定向至微信授权URL


                //重定向至微信领奖页url，把奖品信息参数包裹
                string url = string.Format("{0}?activityId={1}&giftId={2}&giftType={3}", wx_url, activityId, awardid, awardname);

                //url encode编码
                string urlencode = System.Web.HttpUtility.UrlEncode(url, System.Text.Encoding.UTF8);

                //重定向 微信授权url
                ResponseWXRedirect(urlencode);

                #endregion

                return empty;
            }
            catch (Exception ex)
            {
                Common.Helper.Logger.Info(string.Format("{0}进入领奖页面异常,异常信息：{1}", str_a_activityname, ex.ToString()));
                return View("Nogift");
            }
        }


        /// <summary>
        /// 微信控制器
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult WXActivity(RequestModel request)
        {
            ActionResult empty = new EmptyResult();

            //从参数中获取奖品id，class
            AwardsInfoModel awardsModel = new AwardsInfoModel();
            request.giftId = DESEncrypt.Encrypt(request.giftId, _key);
            awardsModel.id = request.giftId;
            awardsModel.Class = request.giftType;

            try
            {
                #region 数据库日志记录  公共部分

                TRP_ClientLog entity = new TRP_ClientLog();
                entity.CreateTime = DateTime.Now;
                entity.DeleteMark = false;
                entity.Enable = true;
                entity.PageUrl = HttpContext.Request.Url.AbsoluteUri ?? wx_url;
                entity.IPAddress = HttpContext.Request.UserHostAddress ?? "127.0.0.1";
                entity.ActivityId = Convert.ToInt32(request.activityId);

                #endregion



                #region 获取微信用户信息 昵称，头像等

                wxUserInfoModel wxUser = GetWxUserInfo(request.code);

                if (wxUser == null)
                {
                    //微信用户信息未获取到 重定向至活动入口Action
                    string url = string.Format("{0}?activityId={1}", enter_url, request.activityId);
                    return empty;
                }

                #endregion

                #region 今天已参加活动

                if (isAttendToday(request.activityId, wxUser.Openid,2))
                {
                    //判断奖品是否已领
                    var receivedModel = awardDi.getBll().hadTakeAward(wxUser.Openid, request.activityId);

                    #region 奖品已核销

                    //奖品已领
                    if (receivedModel == null)
                    {
                        scanCountDi.getBll().CountByNameAndId(request.activityId, str_d_activityname);
                        return View("HadAttend");
                    }
                    #endregion

                    #region 奖品还未核销 返回Activity视图

                    //奖品还未领取
                    else
                    {
                        //获取奖品ID  int
                        string awardId = receivedModel.AwardDetailId.ToString();

                        //获取奖品详情
                        TRP_AwardDetail detailModel = detailDi.getBll().GetEntityById(awardId);

                        //奖品名称
                        string awardsName = "";

                        //加密奖品id
                        string ecodeAwardId = DESEncrypt.Encrypt(awardId, _key);


                        if (detailModel != null)
                        {
                            awardsName = detailModel.AwardName;
                        }

                        //奖品类型
                        if (!string.IsNullOrWhiteSpace(awardsName))
                        {
                            string typeCode = "";
                            if (awards_code.ContainsKey(awardsName.Trim()))
                            {
                                typeCode = awards_code[awardsName.Trim()];
                            }
                            else
                            {
                                return View("Nogift");
                            }
                            ViewData["Type"] = typeCode;
                        }

                        ViewData["Openid"] = wxUser.Openid ?? "";
                        ViewData["wxName"] = wxUser.Nickname ?? "";
                        ViewData["AwardDetailId"] = ecodeAwardId;
                        ViewData["AwardName"] = awardsName;
                        ViewData["Activity"] = request.activityId ?? "";

                        entity.Description = string.Format("用户在{0}点击红包，二维码实物扫码（为重复扫码,上次未核销奖品）", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        entity.PageDesc = "实物扫码,上次未核销奖品";
                        entity.ActivityId = Convert.ToInt32(request.activityId);
                        logDi.getBll().SaveLog(entity);
                        return View("Activity");
                    }
                    #endregion
                }

                #endregion

                #region 首次参加活动
                else
                {
                    #region 奖品已有意中人
                    if (isGetAward(request.activityId, request.giftId))
                    {
                        return View("Nogift");
                    }
                    #endregion

                    else
                    {
                        //从参数中获取奖品id，class
                        //AwardsInfoModel awardsModel = new AwardsInfoModel();
                        //awardsModel.id = request.giftId;
                        //awardsModel.Class = request.giftType;

                        //奖品的Class不为空
                        if (awardsModel.Class != null)
                        {
                            if (awardsModel.Class == "")
                            {
                                scanCountDi.getBll().CountByNameAndId(request.activityId, str_e_activityname);
                                return View("Nogift");
                            }
                            else
                            {

                                string awardsType = awardsModel.Class;
                                string typeCode = "";

                                //Common.Helper.Logger.Info("--test--");
                                //Common.Helper.Logger.Info(awardsModel.Class);
                                //Common.Helper.Logger.Info(awards_code.ContainsKey(awardsModel.Class.Trim()));
                                if (awards_code.ContainsKey(awardsModel.Class.Trim()))
                                {
                                    //Common.Helper.Logger.Info(awards_code[awardsModel.Class.Trim()]);
                                    typeCode = awards_code[awardsModel.Class.Trim()];
                                }
                                else
                                {
                                    return View("Nogift");
                                }
                                ViewData["Type"] = typeCode;

                                //不存在微信用户
                                if (!isExistOpenId(wxUser.Openid))
                                {
                                    //保存用户微信信息
                                    saveUserInfo(wxUser);
                                }

                                //保存扫码信息
                                saveScanInfo(wxUser.Openid, awardsModel.id);

                                string awardName = awardsType;
                                entity.Description = string.Format("{0},用户在{1}点击红包，二维码扫码进入实物领奖页面，奖品为{1}", str_a_activityname, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), awardsType);
                                entity.PageDesc = string.Format("实物扫码,得到{0}", awardName);
                                entity.ActivityId = int.Parse(request.activityId);
                                logDi.getBll().SaveLog(entity);

                                ViewData["Openid"] = wxUser.Openid ?? "";
                                ViewData["wxName"] = wxUser.Nickname ?? "";
                                ViewData["AwardDetailId"] = awardsModel.id ?? "";
                                ViewData["AwardName"] = awardsModel.Class ?? "";
                                ViewData["Activity"] = request.activityId ?? "";
                                return View("Activity");
                            }
                        }
                        else
                        {
                            scanCountDi.getBll().CountByNameAndId(request.activityId, str_f_activityname);
                            return View("Nogift");
                        }
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.Helper.Logger.Info(string.Format("{0}领取实物异常,异常信息：{1}", str_a_activityname, ex.ToString()));
                Response.Redirect(string.Format("{0}?activityId={1}&flag={2}", enter_url, request.activityId, 2));
                return empty;
            }
        }


        /// <summary>
        /// 红包-核销领奖(重写)
        /// </summary>
        /// <param name="Openid"></param>
        /// <param name="AwardDetailId"></param>
        /// <returns></returns>
        [HttpPost]
        public override JsonResult ReceivedAward(AwardReceivedModel model)
        {
            object data = null;
            try
            {
                var res = new JsonResult();
                if (saveUserAwardReceiveInfo(model.Openid, model.AwardDetailId))
                {
                    //返回json
                    data = new { success = true, status = "1" };

                    //记录日志
                    TRP_ClientLog entity = new TRP_ClientLog();
                    entity.CreateTime = DateTime.Now;
                    entity.DeleteMark = false;
                    entity.Enable = true;
                    entity.PageUrl = HttpContext.Request.Url.AbsoluteUri ?? "http://www.chinazhihuiping.com/wxredpackets";
                    entity.IPAddress = HttpContext.Request.UserHostAddress ?? "127.0.0.1";
                    var efid = DESEncrypt.Decrypt(model.AwardDetailId, _key);
                    var awardName = model.AwardName;
                    entity.Description = string.Format("{0}工作人员核销领奖，奖品为{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), awardName);
                    entity.PageDesc = string.Format("工作人员核销领奖,奖品{0}", awardName);
                    entity.ActivityId = Convert.ToInt32(model.ActivityId);

                    //保存日志
                    logDi.getBll().SaveLog(entity);
                }
                else
                {
                    //返回json
                    data = new { success = false, status = "1" };
                }
            }
            catch (Exception ex)
            {
                Common.Helper.Logger.Info(string.Format("微信用户-OpenId:{0}-领取奖品，保存到数据库中出现错误,，奖品ID为{1}，错误详情:{2}",
                    model.Openid,
                    model.AwardDetailId,
                    ex.ToString()));
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 跳转计数
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectCount(string url, string goods)
        {
            ActionResult empty = new EmptyResult();
            try
            {
                scanCountDi.getBll().CountByUrl(url, goods);
                //获取奖品的类型实现奖品页面的跳转
                ResponseRedirect(url);
                return empty;
            }
            catch (Exception ex)
            {
                Common.Helper.Logger.Info(string.Format("" + url + "页面的跳转异常,异常信息：{0}", ex.ToString()));
                ResponseRedirect(url);
                return empty;
            }
        }

    }
}