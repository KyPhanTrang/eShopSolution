<script setup>
import { ref, computed } from 'vue';
import { useI18n } from 'vue-i18n';
import { languages } from '@/types/language';

const { locale } = useI18n();

const currentLang = computed(() => {
  return languages.find((lang) => lang.code === locale.value);
});

const changeLanguage = (lang) => {
  locale.value = lang;
  localStorage.setItem('lang', lang);
};
</script>

<template>
  <div class="header_box">
    <div class="lang_box">
      <a href="#" title="Language" class="nav-link" data-toggle="dropdown" aria-expanded="true">
        <img :src="currentLang?.flag" alt="flag" class="mr-2" title="United Kingdom" />
        {{ currentLang?.label }} <i class="fa fa-angle-down ml-2" aria-hidden="true"></i>
      </a>
      <div class="dropdown-menu">
        <a
          v-for="lang in languages"
          :key="lang.code"
          href="#"
          class="dropdown-item"
          @click="changeLanguage(lang.code)"
        >
          <img :src="lang.flag" class="mr-2" alt="flag" />
          {{ lang.label }}
        </a>
      </div>
    </div>
    <div class="login_menu">
      <ul>
        <li>
          <a href="#">
            <i class="fa fa-shopping-cart" aria-hidden="true"></i>
            <span class="padding_10">{{ $t('header.action.cart') }}</span></a
          >
        </li>
        <li>
          <a href="#">
            <i class="fa fa-user" aria-hidden="true"></i>
            <span class="padding_10">{{ $t('header.action.login') }}</span></a
          >
        </li>
      </ul>
    </div>
  </div>
</template>

<style scoped>
.lang_box {
  position: relative;
}
.dropdown-menu {
  left: 0 !important;
  top: 100% !important;
  transform: none !important;
}
</style>
