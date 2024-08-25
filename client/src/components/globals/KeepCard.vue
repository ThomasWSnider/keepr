<script setup>
import { AppState } from "@/AppState";
import { Keep } from "@/models/Keep";
import { keepsService } from "@/services/KeepsService";
import Pop from "@/utils/Pop";
import { computed } from "vue";

const account = computed(() => AppState.account)
defineProps({ keep: Keep })

function activateKeep(keepId) {
  keepsService.activateKeep(keepId)
}

async function destroyKeep(keepId) {
  try {
    keepsService.destroyKeep(keepId)
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div class="position-relative keep-container mb-3">
    <i v-if="account?.id == keep.creator.id" @click="destroyKeep(keep.id)" role="button" class="mdi mdi-close-circle"
      :title="`Delete ${keep.name}`"></i>

    <div @click="activateKeep(keep.id)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <img class=" img-fluid rounded shadow" :src="keep.img" :alt="keep.name" :title="`View ${keep.name}`">

      <div class=" keep-flavor d-flex align-items-center justify-content-between">
        <p class="fs-2 fw-bold text-light text-meriweather-bold">{{ keep.name }}</p>
      </div>
    </div>

    <div class="selectable creator-flavor">
      <img class="img-fluid creator-img" :src="keep.creator.picture" :alt="keep.creator.name"
        :title="`Go To ${keep.creator.name}'s profile page`">
    </div>

  </div>
</template>


<style lang="scss" scoped>
.keep-flavor {
  position: absolute;
  width: 66%;
  bottom: 5px;
  left: 10px;
}

.keep-container {
  &:hover {
    i {
      display: inline-block;
    }
  }
}

.creator-img {
  height: 6dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.creator-flavor {
  position: absolute;
  bottom: 5px;
  right: 10px;
}

i {
  position: absolute;
  display: none;
  top: -8px;
  right: -5px;
  color: var(--bs-danger);
  z-index: 1;

  &:hover {
    cursor: pointer;
  }
}

p {
  -webkit-text-stroke-width: 1px;
  -webkit-text-stroke-color: #040404;
  margin: 0;
}
</style>