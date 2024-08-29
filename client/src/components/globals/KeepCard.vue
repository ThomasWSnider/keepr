<script setup>
import { AppState } from "@/AppState";
import { Keep } from "@/models/Keep";
import { Kept } from "@/models/VaultKeep";
import { keepsService } from "@/services/KeepsService";
import Pop from "@/utils/Pop";
import { computed } from "vue";

const account = computed(() => AppState.account)
defineProps({ keep: [Kept, Keep], onProfile: Boolean })


async function activateKeep(keep) {
  try {
    await keepsService.activateKeep(keep, account.value.id)
  } catch (error) {
    Pop.error(error);
  }
}

async function destroyKeep(keepId) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to delete this keep?", "It will be gone forever", "Confirm")
    if (!confirm) return
    keepsService.destroyKeep(keepId)
  } catch (error) {
    Pop.error(error);
  }
}

</script>


<template>
  <div class="position-relative keep-container mb-3">
    <i v-if="account?.id == keep.creator.id && !keep.vaultKeepId" @click="destroyKeep(keep.id)" role="button"
      class="mdi mdi-close-circle" :title="`Delete ${keep.name}`"></i>
    <div @click="activateKeep(keep)" class="selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
      <img class=" img-fluid rounded shadow" :src="keep.img" :alt="keep.name" :title="`View ${keep.name}`">
      <div class=" keep-flavor d-flex align-items-center justify-content-between">
        <p class="fs-2 fw-bold text-light text-meriweather-bold">{{ keep.name }}</p>
      </div>
    </div>
    <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
      <div v-if="!onProfile" class="selectable creator-flavor">
        <img class="img-fluid creator-img" :src="keep.creator.picture" :alt="keep.creator.name"
          :title="`Go To ${keep.creator.name}'s profile page`">
      </div>
    </RouterLink>

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
  overflow-wrap: break-word;
  margin: 0;
  overflow: hidden;
  max-height: 3em;
}
</style>