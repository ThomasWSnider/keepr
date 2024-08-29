<script setup>
import { vaultsService } from "@/services/VaultsService";
import Pop from "@/utils/Pop";
import { Modal } from "bootstrap";
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter()

const editableVaultData = ref({
  name: "",
  img: "",
  isPrivate: false
})

async function createNewVault() {
  try {
    const newVault = await vaultsService.CreateNewVault(editableVaultData.value)
    editableVaultData.value = {
      name: "",
      img: "",
      isPrivate: false
    }
    Modal.getOrCreateInstance("#newVaultFormModal").hide()
    router.push({ name: 'Vault', params: { vaultId: newVault.id } })
  } catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <div class="modal-body">
    <div class="container-fluid">
      <form @submit.prevent="createNewVault()">
        <section class="row justify-content-center">
          <div class="col-12 text-end">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="col-12 text-center">
            <p class="fs-1 fw-semibold">Add New Vault</p>
          </div>
          <div class="col-12">
            <div class="form-floating mb-3">
              <input v-model="editableVaultData.name" type="text" class="form-control" id="name" placeholder="Name"
                maxlength="47" required>
              <label for="name">Name</label>
            </div>
            <div class="form-floating mb-5">
              <input v-model="editableVaultData.img" type="url" class="form-control" id="imgUrl" placeholder="Image Url"
                maxlength="1000" required>
              <label for="imgUrl">Image Url</label>
            </div>
          </div>
        </section>
        <section class="row justify-content-end">
          <div class="col-md-5 col-12 d-flex flex-column justify-content-center text-center">
            <small>Private Vaults can only be seen by you</small>
            <div class="form-check d-flex align-items-center justify-content-center">
              <input v-model="editableVaultData.isPrivate" class="form-check-input me-2" type="checkbox" id="isPrivate">
              <label class="form-check-label fs-4" for="isPrivate">
                Make Vault Private
              </label>
            </div>
            <div class="d-grid mt-3">
              <button class="btn btn-secondary fs-5 fw-bold" type="submit">Create Vault</button>
            </div>
          </div>
        </section>
      </form>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>