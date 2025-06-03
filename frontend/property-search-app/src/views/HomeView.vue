<script setup lang="ts">
import { onMounted } from 'vue'
import { usePropertyStore } from '@/stores/counter'
import PropertySearchFilters from '@/components/PropertySearchFilters.vue'
import PropertyGrid from '@/components/PropertyGrid.vue'

const propertyStore = usePropertyStore()

onMounted(async () => {
  await propertyStore.loadFilterOptions()
  await propertyStore.searchProperties()
})
</script>

<template>
  <div class="property-search-page">
    <div class="container">
      <!-- Hero Section -->
      <section class="hero-section">
        <div class="hero-content">
          <h1 class="hero-title">Find Your Dream Property</h1>
          <p class="hero-subtitle">
            Discover the perfect home with our advanced search filters and comprehensive property database
          </p>
          <div class="hero-stats">
            <div class="stat-item">
              <span class="stat-number">10,000+</span>
              <span class="stat-label">Properties</span>
            </div>
            <div class="stat-item">
              <span class="stat-number">50+</span>
              <span class="stat-label">Locations</span>
            </div>
            <div class="stat-item">
              <span class="stat-number">24/7</span>
              <span class="stat-label">Support</span>
            </div>
          </div>
        </div>
      </section>

      <!-- Search Section -->
      <section class="search-section">
        <PropertySearchFilters />
      </section>

      <!-- Results Section -->
      <section class="results-section">
        <PropertyGrid />
      </section>
    </div>
  </div>
</template>

<style scoped>
.property-search-page {
  min-height: calc(100vh - 200px);
}

/* Hero Section */
.hero-section {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: var(--spacing-xxl) 0;
  margin: calc(var(--spacing-lg) * -1) calc(var(--spacing-lg) * -1) var(--spacing-xl);
  border-radius: 0 0 var(--radius-xl) var(--radius-xl);
  text-align: center;
}

.hero-content {
  max-width: 800px;
  margin: 0 auto;
  padding: 0 var(--spacing-lg);
}

.hero-title {
  font-size: var(--font-4xl);
  font-weight: 700;
  margin-bottom: var(--spacing-md);
  line-height: 1.2;
}

.hero-subtitle {
  font-size: var(--font-lg);
  margin-bottom: var(--spacing-xl);
  opacity: 0.9;
  line-height: 1.6;
}

.hero-stats {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
  gap: var(--spacing-lg);
  max-width: 500px;
  margin: 0 auto;
}

.stat-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--spacing-xs);
}

.stat-number {
  font-size: var(--font-2xl);
  font-weight: 700;
  color: #fff;
}

.stat-label {
  font-size: var(--font-sm);
  opacity: 0.8;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

/* Search Section */
.search-section {
  margin-bottom: var(--spacing-xl);
}

/* Results Section */
.results-section {
  margin-bottom: var(--spacing-xl);
}

/* 响应式调整 */
@media (max-width: 991.98px) {
  .hero-section {
    padding: var(--spacing-xl) 0;
    margin: calc(var(--spacing-md) * -1) calc(var(--spacing-md) * -1) var(--spacing-lg);
  }
  
  .hero-title {
    font-size: var(--font-3xl);
  }
  
  .hero-subtitle {
    font-size: var(--font-base);
  }
  
  .hero-stats {
    gap: var(--spacing-md);
  }
}

@media (max-width: 767.98px) {
  .hero-section {
    padding: var(--spacing-lg) 0;
    margin: calc(var(--spacing-md) * -1) calc(var(--spacing-md) * -1) var(--spacing-md);
    border-radius: 0 0 var(--radius-lg) var(--radius-lg);
  }
  
  .hero-content {
    padding: 0 var(--spacing-md);
  }
  
  .hero-title {
    font-size: var(--font-2xl);
  }
  
  .hero-stats {
    grid-template-columns: repeat(3, 1fr);
    gap: var(--spacing-sm);
  }
  
  .stat-number {
    font-size: var(--font-xl);
  }
  
  .stat-label {
    font-size: var(--font-xs);
  }
  
  .search-section {
    margin-bottom: var(--spacing-lg);
  }
  
  .results-section {
    margin-bottom: var(--spacing-lg);
  }
}

@media (max-width: 575.98px) {
  .hero-section {
    padding: var(--spacing-md) 0;
  }
  
  .hero-title {
    font-size: var(--font-xl);
    margin-bottom: var(--spacing-sm);
  }
  
  .hero-subtitle {
    font-size: var(--font-sm);
    margin-bottom: var(--spacing-lg);
  }
  
  .hero-stats {
    gap: var(--spacing-xs);
  }
  
  .stat-item {
    gap: 2px;
  }
  
  .stat-number {
    font-size: var(--font-lg);
  }
  
  .stat-label {
    font-size: 10px;
  }
}

/* 动画效果 */
@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.hero-content > * {
  animation: fadeInUp 0.6s ease-out;
}

.hero-title {
  animation-delay: 0.1s;
}

.hero-subtitle {
  animation-delay: 0.2s;
}

.hero-stats {
  animation-delay: 0.3s;
}

/* 减少动画（可访问性） */
@media (prefers-reduced-motion: reduce) {
  .hero-content > * {
    animation: none;
  }
}

/* 高对比度模式 */
@media (prefers-contrast: high) {
  .hero-section {
    background: #000;
    color: #fff;
  }
  
  .stat-number {
    color: #fff;
  }
}

/* 暗色模式 */
@media (prefers-color-scheme: dark) {
  .hero-section {
    background: linear-gradient(135deg, #2d3748 0%, #4a5568 100%);
  }
}
</style>
