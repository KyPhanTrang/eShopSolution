<template>
  <div id="mySidenav" class="sidenav" :style="{ width: isOpen ? '250px' : '0' }">
    <a href="#" class="closebtn" @click.prevent="closeNav">&times;</a>
    <a href="index.html">Home</a>
    <a href="fashion.html">Fashion</a>
    <a href="electronic.html">Electronic</a>
    <a href="jewellery.html">Jewellery</a>
  </div>
  <span class="toggle_icon" @click="openNav"><img src="/images/toggle-icon.png" /></span>
</template>

<script setup>
import { ref } from 'vue';

const isOpen = ref(false);

const openNav = () => {
  isOpen.value = true;
};

const closeNav = () => {
  isOpen.value = false;
};
</script>
