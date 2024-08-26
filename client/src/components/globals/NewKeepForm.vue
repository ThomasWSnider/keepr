<script setup>
import { computed, ref } from "vue";
import { AppState } from "@/AppState";
import Pop from "@/utils/Pop";
import { keepsService } from "@/services/KeepsService";
import { Modal } from "bootstrap";

const account = computed(() => AppState.account)

const editableKeepData = ref({
  name: "",
  description: "",
  img: ""
})

async function createNewKeep() {
  try {
    await keepsService.createNewKeep(editableKeepData.value)
    editableKeepData.value = {
      name: "",
      description: "",
      img: ""
    }
    Modal.getOrCreateInstance("#newKeepFormModal").hide()
  } catch (error) {
    Pop.error(error);
  }
}
</script>

<template>
  <div v-if="account" class="modal-body">
    <div class="container-fluid">
      <section class="row">
        <div class="col-12 text-end">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="col-12">
          <p class="fs-1 fw-semibold">Add your keep</p>
        </div>

        <form @submit.prevent="createNewKeep()" class="row">
          <div class="col-12 order-1">
            <div class="mb-3 form-floating">
              <input v-model="editableKeepData.img" type="url" class="form-control" id="img"
                placeholder="Paste Image Here" maxlength="1000" required>
              <label for="img" class="form-label">Image</label>
            </div>
          </div>
          <div class="col-12 order-2">
            <div class="mb-3 form-floating">
              <input v-model="editableKeepData.name" type="text" class="form-control" id="name"
                placeholder="Name Your Keep" maxlength="47" required>
              <label for="name" class="form-label">Title</label>
            </div>
          </div>
          <div class="col-lg-6 col-12 order-md-3 order-4">
            <div class="mb-5 form-floating">
              <textarea v-model="editableKeepData.description" class="form-control" id="description"
                placeholder="Add Description" maxlength="1000" required></textarea>
              <label for="description" class="form-label">Description</label>
            </div>
          </div>
          <div class="col-lg-6 col-12 order-md-4 order-3">
            <div class="mb-5 img-preview d-flex justify-content-center align-items-center">
              <div class="keep-container">
                <div class="position-relative">
                  <img v-if="editableKeepData.img" class="img-fluid rounded shadow keep-img" :src="editableKeepData.img"
                    alt="Image Preview" :title="`${editableKeepData.name}`">
                  <img v-else class="rounded shadow preview-img" src="/public/img/Placeholder.png"
                    alt="Add an Image URL">
                  <div v-if="editableKeepData.img"
                    class=" keep-flavor d-flex align-items-start justify-content-between">
                    <p class="fs-2 fw-bold text-light text-meriweather-bold preview-text">{{ editableKeepData.name }}
                    </p>
                  </div>
                  <div class="creator-flavor">
                    <img class="creator-img" :src="account.picture" :alt="account.name">
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-12 order-5 text-end mb-3">
            <button type="submit" class="btn btn-secondary">Create</button>
          </div>
        </form>

      </section>
    </div>
  </div>
</template>

<style lang="scss" scoped>
textarea {
  min-height: 18em !important;
  resize: none;

  @media (max-width: 991.98px) {
    min-height: 10em !important;
    resize: vertical;
  }
}

.preview-text {
  -webkit-text-stroke-width: 1px;
  -webkit-text-stroke-color: #040404;
  overflow-wrap: break-word;
  margin: 0;
  overflow: hidden;
}

.img-preview {
  height: 18em;

  @media (max-width: 991.98px) {
    height: 10em;
  }
}

.keep-img {
  max-height: 18em;
  width: 100%;
  object-fit: contain;
  object-position: center;

  @media (max-width: 991.98px) {
    max-height: 10em;
  }
}

.preview-img {
  height: 12em;
  width: 100%;
  object-fit: contain;
  object-position: center;

  @media (max-width: 991.98px) {
    max-height: 10em;
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

.keep-flavor {
  position: absolute;
  width: 70%;
  max-height: 85%;
  bottom: 5px;
  left: 10px;
  overflow: hidden;
}
</style>