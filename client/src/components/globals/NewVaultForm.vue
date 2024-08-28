<script setup>

</script>


<template>
  <div class="modal-body">
    <div class="container-fluid">
      <section class="row justify-content-center">
        <div class="col-12 text-end">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="col-12 text-center">
          <p class="fs-1 fw-semibold">Add New Vault</p>
        </div>
        <div class="col-12">
          <div class="form-floating mb-3">
            <input type="text" class="form-control" id="name" placeholder="Name">
            <label for="name">Name</label>
          </div>
          <div class="form-floating mb-5">
            <input type="url" class="form-control" id="imgUrl" placeholder="Image Url">
            <label for="imgUrl">Image Url</label>
          </div>
        </div>
      </section>
      <section class="row justify-content-end">
        <div class="col-5 d-flex flex-column justify-content-center text-center">
          <small>Private Vaults can only be seen by you</small>
          <div class="form-check d-flex align-items-center justify-content-center">
            <input class="form-check-input me-2" type="checkbox" value="" id="isPrivate">
            <label class="form-check-label fs-4" for="isPrivate">
              Make Vault Private
            </label>
          </div>
          <div class="d-grid mt-3">
            <button class="btn btn-secondary fs-5 fw-bold">Create Vault</button>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>