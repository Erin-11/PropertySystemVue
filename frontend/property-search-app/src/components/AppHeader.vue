<template>
  <header class="app-header">
    <div class="container">
      <nav class="navbar">
        <!-- Logo/Brand -->
        <div class="navbar-brand">
          <RouterLink to="/" class="brand-link">
            <img 
              alt="Property Search Logo" 
              class="brand-logo" 
              src="@/assets/logo.svg" 
              width="40" 
              height="40" 
            />
            <span class="brand-text">PropertySearch</span>
          </RouterLink>
        </div>

        <!-- Desktop Navigation -->
        <ul class="navbar-nav d-none d-md-flex">
          <li class="nav-item">
            <RouterLink to="/" class="nav-link">
              <span class="nav-icon">🏠</span>
              <span>Home</span>
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/about" class="nav-link">
              <span class="nav-icon">ℹ️</span>
              <span>About</span>
            </RouterLink>
          </li>
          <li class="nav-item">
            <a href="#" class="nav-link">
              <span class="nav-icon">📞</span>
              <span>Contact</span>
            </a>
          </li>
        </ul>

        <!-- Mobile Menu Button -->
        <button 
          class="mobile-menu-btn d-md-none"
          @click="toggleMobileMenu"
          :aria-expanded="isMobileMenuOpen"
          aria-label="Toggle navigation menu"
        >
          <span class="hamburger-line" :class="{ active: isMobileMenuOpen }"></span>
          <span class="hamburger-line" :class="{ active: isMobileMenuOpen }"></span>
          <span class="hamburger-line" :class="{ active: isMobileMenuOpen }"></span>
        </button>
      </nav>

      <!-- Mobile Navigation Menu -->
      <div class="mobile-menu d-md-none" :class="{ active: isMobileMenuOpen }">
        <ul class="mobile-nav">
          <li class="mobile-nav-item">
            <RouterLink to="/" class="mobile-nav-link" @click="closeMobileMenu">
              <span class="nav-icon">🏠</span>
              <span>Home</span>
            </RouterLink>
          </li>
          <li class="mobile-nav-item">
            <RouterLink to="/about" class="mobile-nav-link" @click="closeMobileMenu">
              <span class="nav-icon">ℹ️</span>
              <span>About</span>
            </RouterLink>
          </li>
          <li class="mobile-nav-item">
            <a href="#" class="mobile-nav-link" @click="closeMobileMenu">
              <span class="nav-icon">📞</span>
              <span>Contact</span>
            </a>
          </li>
        </ul>
      </div>
    </div>

    <!-- Mobile Menu Overlay -->
    <div 
      class="mobile-menu-overlay d-md-none" 
      :class="{ active: isMobileMenuOpen }"
      @click="closeMobileMenu"
    ></div>
  </header>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import { RouterLink } from 'vue-router'

const isMobileMenuOpen = ref(false)

function toggleMobileMenu() {
  isMobileMenuOpen.value = !isMobileMenuOpen.value
  
  // 防止背景滚动
  if (isMobileMenuOpen.value) {
    document.body.style.overflow = 'hidden'
  } else {
    document.body.style.overflow = ''
  }
}

function closeMobileMenu() {
  isMobileMenuOpen.value = false
  document.body.style.overflow = ''
}

// 监听窗口大小变化，大屏幕时关闭移动菜单
function handleResize() {
  if (window.innerWidth >= 768 && isMobileMenuOpen.value) {
    closeMobileMenu()
  }
}

// 监听ESC键关闭菜单
function handleKeydown(event: KeyboardEvent) {
  if (event.key === 'Escape' && isMobileMenuOpen.value) {
    closeMobileMenu()
  }
}

onMounted(() => {
  window.addEventListener('resize', handleResize)
  document.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
  window.removeEventListener('resize', handleResize)
  document.removeEventListener('keydown', handleKeydown)
  document.body.style.overflow = '' // 清理样式
})
</script>

<style scoped>
.app-header {
  background: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  position: sticky;
  top: 0;
  z-index: 1000;
}

.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: var(--spacing-md) 0;
  min-height: 64px;
}

/* Brand/Logo */
.navbar-brand {
  flex-shrink: 0;
}

.brand-link {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: var(--text-primary);
  font-weight: 600;
  font-size: var(--font-lg);
  min-height: auto;
}

.brand-logo {
  margin-right: var(--spacing-sm);
  flex-shrink: 0;
}

.brand-text {
  color: var(--color-primary);
}

/* Desktop Navigation */
.navbar-nav {
  display: flex;
  list-style: none;
  margin: 0;
  padding: 0;
  gap: var(--spacing-lg);
}

.nav-item {
  margin: 0;
}

.nav-link {
  display: flex;
  align-items: center;
  gap: var(--spacing-xs);
  padding: var(--spacing-sm) var(--spacing-md);
  text-decoration: none;
  color: var(--text-secondary);
  font-weight: 500;
  border-radius: var(--radius-md);
  transition: var(--transition-fast);
  min-height: 44px;
}

.nav-link:hover {
  color: var(--color-primary);
  background-color: rgba(0, 123, 255, 0.1);
}

.nav-link.router-link-active {
  color: var(--color-primary);
  background-color: rgba(0, 123, 255, 0.1);
}

.nav-icon {
  font-size: var(--font-base);
}

/* Mobile Menu Button */
.mobile-menu-btn {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 44px;
  height: 44px;
  background: none;
  border: none;
  cursor: pointer;
  padding: var(--spacing-sm);
  border-radius: var(--radius-md);
  transition: var(--transition-fast);
}

.mobile-menu-btn:hover {
  background-color: var(--color-light);
}

.hamburger-line {
  width: 24px;
  height: 2px;
  background-color: var(--text-primary);
  transition: var(--transition-fast);
  transform-origin: center;
}

.hamburger-line:not(:last-child) {
  margin-bottom: 4px;
}

/* Hamburger Animation */
.hamburger-line.active:nth-child(1) {
  transform: rotate(45deg) translate(5px, 5px);
}

.hamburger-line.active:nth-child(2) {
  opacity: 0;
}

.hamburger-line.active:nth-child(3) {
  transform: rotate(-45deg) translate(7px, -6px);
}

/* Mobile Menu */
.mobile-menu {
  position: fixed;
  top: 64px;
  left: 0;
  right: 0;
  background: #fff;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transform: translateY(-100%);
  transition: transform var(--transition-normal);
  z-index: 999;
  max-height: calc(100vh - 64px);
  overflow-y: auto;
}

.mobile-menu.active {
  transform: translateY(0);
}

.mobile-nav {
  list-style: none;
  margin: 0;
  padding: var(--spacing-md) 0;
}

.mobile-nav-item {
  margin: 0;
}

.mobile-nav-link {
  display: flex;
  align-items: center;
  gap: var(--spacing-md);
  padding: var(--spacing-md) var(--spacing-lg);
  text-decoration: none;
  color: var(--text-secondary);
  font-weight: 500;
  font-size: var(--font-lg);
  transition: var(--transition-fast);
  min-height: 56px;
  border-bottom: 1px solid var(--border-color);
}

.mobile-nav-link:hover {
  color: var(--color-primary);
  background-color: var(--color-light);
}

.mobile-nav-link.router-link-active {
  color: var(--color-primary);
  background-color: rgba(0, 123, 255, 0.1);
}

/* Mobile Menu Overlay */
.mobile-menu-overlay {
  position: fixed;
  top: 64px;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  opacity: 0;
  visibility: hidden;
  transition: var(--transition-normal);
  z-index: 998;
}

.mobile-menu-overlay.active {
  opacity: 1;
  visibility: visible;
}

/* 响应式调整 */
@media (max-width: 575.98px) {
  .navbar {
    padding: var(--spacing-sm) 0;
    min-height: 56px;
  }
  
  .brand-text {
    display: none; /* 超小屏幕隐藏品牌文字 */
  }
  
  .mobile-menu {
    top: 56px;
  }
  
  .mobile-menu-overlay {
    top: 56px;
  }
}

@media (max-width: 479.98px) {
  .mobile-nav-link {
    padding: var(--spacing-sm) var(--spacing-md);
    font-size: var(--font-base);
    min-height: 48px;
  }
}

/* 高对比度模式支持 */
@media (prefers-contrast: high) {
  .app-header {
    border-bottom: 2px solid var(--text-primary);
  }
  
  .nav-link:hover,
  .mobile-nav-link:hover {
    background-color: var(--text-primary);
    color: #fff;
  }
}

/* 暗色模式支持 */
@media (prefers-color-scheme: dark) {
  .app-header {
    background: #1a1a1a;
    color: #fff;
  }
  
  .brand-link {
    color: #fff;
  }
  
  .nav-link {
    color: #ccc;
  }
  
  .nav-link:hover {
    color: #fff;
    background-color: rgba(255, 255, 255, 0.1);
  }
  
  .mobile-menu {
    background: #1a1a1a;
  }
  
  .mobile-nav-link {
    color: #ccc;
    border-bottom-color: #333;
  }
  
  .mobile-nav-link:hover {
    color: #fff;
    background-color: rgba(255, 255, 255, 0.1);
  }
  
  .hamburger-line {
    background-color: #fff;
  }
}
</style>