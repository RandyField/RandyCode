<template lang='html'>
  <div class='stamp'>
    <img src='../assets/stamp-black.png' alt=''/>
  </div>
</template>

<script>
export default {
   data(){
      return{
        blacklogo:"../assets/stamp-black.png",
        redlogo:"../assets/stamp-red.png",
        whitelogo:"../assets/stamp-white.png"
      }
   }
}
</script>

<style>
  .stamp img{
      width: 30vw;
      width: 30vw;
      position: absolute;
      top: 25vw;
      left: 60vw;
  }
</style>
