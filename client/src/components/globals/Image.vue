<script setup>
import { ref } from "vue";

defineProps({ image: String, alt: String })

let isLoaded = ref(false)

function showImage() {
  isLoaded.value = true
}
</script>


<template>
  <div class="img-wrapper">
    <img @load="showImage" height="10000px" width="305px" :class="{ 'opacity-0': !isLoaded, 'opacity-100': isLoaded }"
      :src="image" :alt="alt">
  </div>
</template>


<style lang="scss" scoped>
.img-wrapper {
  background-color: rgb(222, 222, 222);
  overflow: hidden;
}

img {
  transition: opacity 0.5s ease-in-out;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
</style>