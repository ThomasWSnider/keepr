<script setup>
import { AppState } from "@/AppState";
import { computed } from "vue";

const account = computed(() => AppState.account)
const keep = computed(() => AppState.activeKeep)
</script>


<template>
  <div v-if="keep" class="modal-body container-fluid p-0">
    <section class="row">
      <div class="col-md-6 col-12 p-0">
        <img class="img-fluid rounded-start keep-img" :src="keep.img" :alt="keep.name">
      </div>
      <div class="col-md-6 col-12 d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-center mt-4">
          <p class="fw-semibold fs-5 mb-0 me-5 text-secondary"><i class="mdi mdi-eye-outline"></i> {{ keep.views }}</p>
          <p class="fw-semibold fs-5 mb-0 text-secondary"><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
          </p>
        </div>
        <div>
          <p class="fs-2 fw-bold text-center">{{ keep.name }}</p>
          <p class="px-4">{{ keep.description }}</p>
        </div>
        <div class="d-flex justify-content-between align-items-center mb-3">
          <form v-if="account">
            Select Keep
            <button class="btn btn-secondary px-1 py-0">Save</button>
          </form>
          <div class=" pe-2 d-flex align-items-center justify-content-center">
            <img class="creator-img selectable me-1" :src="keep.creator.picture" :alt="keep.creator.name"
              :title="`Go to ${keep.creator.name}'s profile page`">
            <p class="mb-0 fw-semibold">{{ keep.creator.name }}</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>


<style lang="scss" scoped>
.creator-img {
  height: 5dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.keep-img {
  height: 80dvh;
  object-fit: cover;
}
</style>