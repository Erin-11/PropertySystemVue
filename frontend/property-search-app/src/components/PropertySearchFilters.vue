<template>
  <div class="search-filters">
    <div class="filters-container">
      <div class="filter-group">
        <label for="region">Region:</label>
        <select 
          id="region" 
          v-model="propertyStore.selectedRegion"
          @change="onFilterChange"
        >
          <option value="">All Regions</option>
          <option 
            v-for="region in propertyStore.filterOptions?.regions" 
            :key="region" 
            :value="region"
          >
            {{ region }}
          </option>
        </select>
      </div>

      <div class="filter-group">
        <label for="district">District:</label>
        <select 
          id="district" 
          v-model="propertyStore.selectedDistrict"
          @change="onFilterChange"
        >
          <option value="">All Districts</option>
          <option 
            v-for="district in propertyStore.filterOptions?.districts" 
            :key="district" 
            :value="district"
          >
            {{ district }}
          </option>
        </select>
      </div>

      <div class="filter-group">
        <label for="propertyType">Property Type:</label>
        <select 
          id="propertyType" 
          v-model="propertyStore.selectedPropertyType"
          @change="onFilterChange"
        >
          <option value="">All Types</option>
          <option 
            v-for="type in propertyStore.filterOptions?.propertyTypes" 
            :key="type" 
            :value="type"
          >
            {{ type }}
          </option>
        </select>
      </div>

      <div class="filter-group">
        <label for="priceRange">Sale Price:</label>
        <select 
          id="priceRange" 
          @change="onPriceRangeChange"
        >
          <option value="">All Prices</option>
          <option 
            v-for="range in propertyStore.filterOptions?.priceRanges" 
            :key="range.label" 
            :value="JSON.stringify({ min: range.min, max: range.max })"
          >
            {{ range.label }}
          </option>
        </select>
      </div>

      <div class="filter-actions">
        <button 
          @click="onSearch" 
          class="search-btn"
          :disabled="propertyStore.loading"
        >
          {{ propertyStore.loading ? 'Searching...' : 'Search' }}
        </button>
        <button 
          @click="onReset" 
          class="reset-btn"
          :disabled="propertyStore.loading"
        >
          Reset
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { usePropertyStore } from '@/stores/counter'

const propertyStore = usePropertyStore()

function onFilterChange() {
  propertyStore.currentPage = 1
}

function onPriceRangeChange(event: Event) {
  const target = event.target as HTMLSelectElement
  if (target.value) {
    const priceRange = JSON.parse(target.value)
    propertyStore.setPriceRange(priceRange)
  } else {
    propertyStore.setPriceRange({})
  }
}

function onSearch() {
  propertyStore.searchProperties()
}

function onReset() {
  propertyStore.resetFilters()
  propertyStore.searchProperties()
}
</script>

<style scoped>
.search-filters {
  background: white;
  padding: var(--spacing-xl);
  border-radius: var(--radius-lg);
  margin-bottom: var(--spacing-xl);
  box-shadow: var(--shadow-lg);
  border: 1px solid var(--border-color);
}

.filters-container {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
  gap: var(--spacing-lg);
  align-items: end;
}

.filter-group {
  display: flex;
  flex-direction: column;
  gap: var(--spacing-sm);
}

.filter-group label {
  font-weight: 600;
  color: var(--text-primary);
  font-size: var(--font-sm);
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.filter-group select {
  padding: var(--spacing-md);
  border: 2px solid var(--border-color);
  border-radius: var(--radius-md);
  font-size: var(--font-base);
  background: white;
  color: var(--text-primary);
  transition: var(--transition-fast);
  min-height: 48px; /* 触摸友好 */
  appearance: none;
  background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3e%3cpolyline points='6,9 12,15 18,9'%3e%3c/polyline%3e%3c/svg%3e");
  background-repeat: no-repeat;
  background-position: right var(--spacing-md) center;
  background-size: 16px;
  padding-right: calc(var(--spacing-md) * 2.5);
}

.filter-group select:focus {
  outline: none;
  border-color: var(--color-primary);
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.1);
}

.filter-group select:hover {
  border-color: var(--color-primary);
}

.filter-actions {
  display: flex;
  gap: var(--spacing-md);
  grid-column: 1 / -1; /* 占满整行 */
  justify-content: center;
  margin-top: var(--spacing-md);
}

.search-btn, .reset-btn {
  padding: var(--spacing-md) var(--spacing-xl);
  border: none;
  border-radius: var(--radius-md);
  font-size: var(--font-base);
  font-weight: 600;
  cursor: pointer;
  transition: var(--transition-fast);
  min-height: 48px;
  min-width: 120px;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: var(--spacing-sm);
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.search-btn {
  background: var(--color-primary);
  color: white;
  box-shadow: var(--shadow-md);
}

.search-btn:hover:not(:disabled) {
  background: var(--color-primary-dark);
  transform: translateY(-1px);
  box-shadow: var(--shadow-lg);
}

.search-btn:active:not(:disabled) {
  transform: translateY(0);
}

.search-btn:disabled {
  background: var(--color-secondary);
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.reset-btn {
  background: transparent;
  color: var(--color-secondary);
  border: 2px solid var(--color-secondary);
}

.reset-btn:hover:not(:disabled) {
  background: var(--color-secondary);
  color: white;
  transform: translateY(-1px);
}

.reset-btn:active:not(:disabled) {
  transform: translateY(0);
}

.reset-btn:disabled {
  background: transparent;
  color: var(--text-muted);
  border-color: var(--text-muted);
  cursor: not-allowed;
  transform: none;
}

/* 添加图标 */
.search-btn::before {
  content: "🔍";
  font-size: var(--font-base);
}

.reset-btn::before {
  content: "↻";
  font-size: var(--font-base);
}

/* 响应式调整 */
@media (max-width: 991.98px) {
  .filters-container {
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: var(--spacing-md);
  }
  
  .search-filters {
    padding: var(--spacing-lg);
  }
}

@media (max-width: 767.98px) {
  .search-filters {
    padding: var(--spacing-md);
    border-radius: var(--radius-md);
    margin-bottom: var(--spacing-lg);
  }
  
  .filters-container {
    grid-template-columns: 1fr;
    gap: var(--spacing-md);
  }
  
  .filter-actions {
    flex-direction: column;
    gap: var(--spacing-sm);
    margin-top: var(--spacing-lg);
  }
  
  .search-btn, .reset-btn {
    width: 100%;
    min-width: auto;
  }
  
  .filter-group label {
    font-size: var(--font-xs);
  }
}

@media (max-width: 575.98px) {
  .search-filters {
    padding: var(--spacing-sm);
    margin-bottom: var(--spacing-md);
  }
  
  .filters-container {
    gap: var(--spacing-sm);
  }
  
  .filter-group select {
    padding: var(--spacing-sm) var(--spacing-md);
    font-size: var(--font-sm);
    min-height: 44px;
    background-size: 14px;
    padding-right: calc(var(--spacing-md) * 2);
  }
  
  .search-btn, .reset-btn {
    padding: var(--spacing-sm) var(--spacing-md);
    font-size: var(--font-sm);
    min-height: 44px;
  }
  
  .filter-actions {
    margin-top: var(--spacing-md);
  }
}

/* 加载状态动画 */
.search-btn:disabled::before {
  content: "⏳";
  animation: spin 1s linear infinite;
}

@keyframes spin {
  from { transform: rotate(0deg); }
  to { transform: rotate(360deg); }
}

/* 焦点可见性改进 */
.filter-group select:focus-visible {
  outline: 2px solid var(--color-primary);
  outline-offset: 2px;
}

.search-btn:focus-visible,
.reset-btn:focus-visible {
  outline: 2px solid var(--color-primary);
  outline-offset: 2px;
}

/* 高对比度模式 */
@media (prefers-contrast: high) {
  .search-filters {
    border: 2px solid var(--text-primary);
  }
  
  .filter-group select {
    border: 2px solid var(--text-primary);
  }
  
  .search-btn {
    background: var(--text-primary);
    border: 2px solid var(--text-primary);
  }
  
  .reset-btn {
    border: 2px solid var(--text-primary);
    color: var(--text-primary);
  }
}

/* 暗色模式 */
@media (prefers-color-scheme: dark) {
  .search-filters {
    background: #2d3748;
    border-color: #4a5568;
  }
  
  .filter-group select {
    background: #2d3748;
    color: #fff;
    border-color: #4a5568;
  }
  
  .filter-group label {
    color: #e2e8f0;
  }
}

/* 减少动画（可访问性） */
@media (prefers-reduced-motion: reduce) {
  .search-btn, .reset-btn {
    transition: none;
  }
  
  .search-btn:hover:not(:disabled),
  .reset-btn:hover:not(:disabled) {
    transform: none;
  }
  
  .search-btn:disabled::before {
    animation: none;
  }
}
</style>