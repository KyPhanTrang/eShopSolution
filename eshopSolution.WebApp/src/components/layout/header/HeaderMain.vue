<template>
  <div class="header_section">
    <div class="container">
      <div class="containt_main">
        <HeaderSidebar />
        <HeaderCategoryBoxes />
        <HeaderSearch />
        <HeaderAction />
      </div>
    </div>
  </div>
</template>

<script setup>
import HeaderSidebar from './header-main/HeaderSidebar.vue'
import HeaderCategoryBoxes from './header-main/HeaderCategoryBoxes.vue'
import HeaderSearch from './header-main/HeaderSearch.vue'
import HeaderAction from './header-main/HeaderAction.vue'
</script>
