<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppHeader from './components/AppHeader.vue'
</script>

<template>
  <div id="app">
    <AppHeader />
    
    <main class="main-content">
      <RouterView />
    </main>
    
    <footer class="app-footer">
      <div class="container">
        <div class="footer-content">
          <div class="footer-section">
            <h4>PropertySearch</h4>
            <p>Find your perfect property with our advanced search platform.</p>
          </div>
          
          <div class="footer-section">
            <h4>Quick Links</h4>
            <ul class="footer-links">
              <li><a href="/">Home</a></li>
              <li><a href="/about">About</a></li>
              <li><a href="#contact">Contact</a></li>
            </ul>
          </div>
          
          <div class="footer-section">
            <h4>Contact Info</h4>
            <p>📧 info@propertysearch.com</p>
            <p>📞 +1 (555) 123-4567</p>
          </div>
        </div>
        
        <div class="footer-bottom">
          <p>&copy; 2024 PropertySearch. All rights reserved.</p>
        </div>
      </div>
    </footer>
  </div>
</template>

<style>
/* 导入响应式系统 */
@import './assets/responsive.css';

/* 应用全局样式 */
#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.main-content {
  flex: 1;
  padding: var(--spacing-lg) 0;
}

/* Footer样式 */
.app-footer {
  background: var(--color-dark);
  color: var(--text-light);
  margin-top: auto;
  padding: var(--spacing-xl) 0 var(--spacing-lg);
}

.footer-content {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: var(--spacing-xl);
  margin-bottom: var(--spacing-lg);
}

.footer-section h4 {
  color: var(--text-light);
  margin-bottom: var(--spacing-md);
  font-size: var(--font-lg);
}

.footer-section p {
  color: #ccc;
  line-height: 1.6;
  margin-bottom: var(--spacing-sm);
}

.footer-links {
  list-style: none;
  padding: 0;
  margin: 0;
}

.footer-links li {
  margin-bottom: var(--spacing-sm);
}

.footer-links a {
  color: #ccc;
  text-decoration: none;
  transition: var(--transition-fast);
  min-height: auto;
}

.footer-links a:hover {
  color: var(--text-light);
}

.footer-bottom {
  border-top: 1px solid #444;
  padding-top: var(--spacing-lg);
  text-align: center;
  color: #999;
}

/* 响应式调整 */
@media (max-width: 767.98px) {
  .main-content {
    padding: var(--spacing-md) 0;
  }
  
  .app-footer {
    padding: var(--spacing-lg) 0 var(--spacing-md);
  }
  
  .footer-content {
    grid-template-columns: 1fr;
    gap: var(--spacing-lg);
    text-align: center;
  }
  
  .footer-bottom {
    margin-top: var(--spacing-md);
  }
}

@media (max-width: 575.98px) {
  .footer-content {
    gap: var(--spacing-md);
  }
  
  .footer-section h4 {
    font-size: var(--font-base);
  }
}

/* 滚动行为优化 */
html {
  scroll-behavior: smooth;
}

/* 选择文本样式 */
::selection {
  background: var(--color-primary);
  color: white;
}

/* 滚动条样式 */
::-webkit-scrollbar {
  width: 8px;
}

::-webkit-scrollbar-track {
  background: #f1f1f1;
}

::-webkit-scrollbar-thumb {
  background: #c1c1c1;
  border-radius: 4px;
}

::-webkit-scrollbar-thumb:hover {
  background: #a8a8a8;
}
</style>
