<template>
  <div class="main">
    <!-- Another variation with a button -->
    <div class="input-group">
      <input type="text" class="form-control" :placeholder="t('header.search.placeholder')" />
      <div class="input-group-append">
        <button
          class="btn btn-secondary"
          type="button"
          style="background-color: #f26522; border-color: #f26522"
        >
          <i class="fa fa-search"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useI18n } from 'vue-i18n';
const { t } = useI18n();
</script>
