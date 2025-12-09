<template>
  <div>
    <Header />
    <main>
      <RouterView />
    </main>
    <!-- Quan trọng -->
    <Footer />
  </div>
</template>

<script setup>
import Header from '@/components/layout/header/Header.vue'
import Footer from '@/components/layout/Footer.vue'
</script>
