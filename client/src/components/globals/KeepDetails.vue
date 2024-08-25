<script setup>
import { AppState } from "@/AppState";
import { computed } from "vue";


const keep = computed(() => AppState.activeKeep)
</script>


<template>
  <div v-if="keep" class="modal-body container-fluid p-0">
    <section class="row">
      <div class="col-md-6 col-12 p-0">
        <img class="img-fluid rounded-start" :src="keep.img" :alt="keep.name">
      </div>
      <div class="col-md-6 col-12 d-flex flex-column">

      </div>
    </section>
  </div>
</template>


<style lang="scss" scoped></style>