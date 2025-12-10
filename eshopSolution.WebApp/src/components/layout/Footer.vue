<template>
  <div class="footer_section layout_padding">
    <div class="container">
      <div class="footer_logo">
        <a><img src="/images/footer-logo.png" /></a>
      </div>
      <div class="input_bt">
        <input type="text" class="mail_bt" placeholder="Your Email" name="Your Email" />
        <span class="subscribe_bt" id="basic-addon2"><a href="#">Subscribe</a></span>
      </div>
      <div class="footer_menu">
        <ul>
          <li>
            <a href="#">{{ $t('header.menu.bestSellers') }}</a>
          </li>
          <li>
            <a href="#">{{ $t('header.menu.giftIdeas') }}</a>
          </li>
          <li>
            <a href="#">{{ $t('header.menu.newReleases') }}</a>
          </li>
          <li>
            <a href="#">{{ $t('header.menu.todayDeals') }}</a>
          </li>
          <li>
            <a href="#">{{ $t('header.menu.customerService') }}</a>
          </li>
        </ul>
      </div>
      <div class="location_main">Help Line Number : <a href="#">+1 1800 1200 1200</a></div>
    </div>
  </div>
  <!-- footer section end -->
  <!-- copyright section start -->
  <div class="copyright_section">
    <div class="container">
      <p class="copyright_text">
        © 2020 All Rights Reserved. Design by <a href="https://html.design">Free html Templates</a>
      </p>
    </div>
  </div>
</template>
