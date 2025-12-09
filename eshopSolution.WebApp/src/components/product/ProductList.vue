<template>
  <div class="jewellery_section">
    <div id="jewellery_main_slider" class="carousel slide" data-ride="carousel">
      <div class="carousel-inner">
        <div class="carousel-item active">
          <div class="container">
            <h1 class="fashion_taital">Man & Woman Fashion</h1>
            <div class="fashion_section_2">
              <div class="row">
                <template v-for="n in 3">
                  <ProductItem />
                </template>
              </div>
            </div>
          </div>
        </div>
        <div class="carousel-item">
          <div class="container">
            <h1 class="fashion_taital">Man & Woman Fashion</h1>
            <div class="fashion_section_2">
              <div class="row">
                <template v-for="n in 3">
                  <ProductItem />
                </template>
              </div>
            </div>
          </div>
        </div>
      </div>
      <a
        class="carousel-control-prev"
        href="#jewellery_main_slider"
        role="button"
        data-slide="prev"
      >
        <i class="fa fa-angle-left"></i>
      </a>
      <a
        class="carousel-control-next"
        href="#jewellery_main_slider"
        role="button"
        data-slide="next"
      >
        <i class="fa fa-angle-right"></i>
      </a>
      <div class="loader_main">
        <div class="loader"></div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ProductItem from './ProductItem.vue'
</script>
