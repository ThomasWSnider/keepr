<script setup>

defineProps({ modalId: String, modalSize: String })
</script>


<template>
  <div class="modal fade" :id="`${modalId}`" tabindex="-1" :aria-labelledby="`${modalId}Label`" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <slot>
        </slot>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>