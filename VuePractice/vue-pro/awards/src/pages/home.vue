<template lang='html'>
  <div class='container'>
    <head-logo></head-logo>
    <words></words>
    <!-- <img-award :awardimg="items.awardimg"></img-award> -->
    <img-award></img-award>
    <stamp></stamp>
  </div>
</template>

<script>
import HeadLogo from '../components/headLogo'
import ImgAward from '../components/imgAward'
import Stamp from '../components/stamp'
import Words from '../components/words'
import {sweetalert} from '../assets/js/sweetalert.min.js'

export default {
    data(){
      return{
        items:{}
      }
    },
    components:{
      HeadLogo,
      ImgAward,
      Stamp,
      Words
    },
    created(){
          swal({
                        title: "提示！",
                        text: "引入sweetalert.min.js成功！",
                        icon: "success",
                        button: false
                    });
      //  this.$http.get('http://47.95.232.44:8088/api/Vue/get').then((result)=>{
      //  this.items=JSON.parse(result.body);
      //  })
    }
}
</script>


<style>
.container{
  width: 100%;
  margin: 0 auto;
}
</style>
