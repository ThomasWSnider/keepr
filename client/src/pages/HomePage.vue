<script setup>
import { AppState } from "@/AppState";
import KeepCard from "@/components/globals/KeepCard.vue";
import KeepDetails from "@/components/globals/KeepDetails.vue";
import ModalWrapper from "@/components/ModalWrapper.vue";
import { keepsService } from "@/services/KeepsService";
import Pop from "@/utils/Pop";
import { computed, onMounted } from "vue";


const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getAllKeeps()
})

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>

<template>
  <div class="container my-5">
    <section class="row">
      <div class="col-12">
        <div class="masonry">
          <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep" :onProfile="false" />
        </div>
      </div>
    </section>
  </div>
  <ModalWrapper modal-id="keepDetailsModal" modal-size="modal-xl modal-fullscreen-sm-down">
    <KeepDetails />
  </ModalWrapper>
</template>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.masonry {
  column-gap: 1em;
  column-fill: balance-all;

  &>* {
    display: inline-block;
  }
}

@media (max-width: 767.98px) {
  .masonry {
    columns: 2;
  }
}

@media (min-width: 768px) {
  .masonry {
    columns: 3;
  }
}

@media (min-width: 991.98px) {
  .masonry {
    columns: 4;
  }
}
</style>
