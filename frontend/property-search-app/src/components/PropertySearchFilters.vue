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

      <div class="filter-group slider-wrap">
        <label for="priceRange">Sale Price:</label>
        <div class="slider-title">
           from
          <div class="slider-value"> {{ priceRange[0] }} </div> to
          <div class="slider-value"> {{ priceRange[1] }} </div>
        </div>
        <el-slider v-model="priceRange"
                   range
                   :min="1000000"
                   :max="100000000"
                   :show-tooltip="false"
                   @change="onSliderChange"
                   class="custom-slider" />
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
  import { ref } from 'vue'
  import { usePropertyStore } from '@/stores/counter'

  const propertyStore = usePropertyStore()
  const priceRange = ref([1000000, 100000000]);

function onFilterChange() {
  propertyStore.currentPage = 1
}

function onSliderChange(value: number[]) {
  propertyStore.setPriceRange({ min: value[0], max: value[1] })
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
  background: #f8f9fa;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.filters-container {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 15px;
  align-items: end;
}

.filter-group {
  display: flex;
  flex-direction: column;
}

.filter-group label {
  font-weight: 600;
  margin-bottom: 5px;
  color: #333;
}

.filter-group select {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  background: white;
}

  .filter-group select:focus {
    outline: none;
    border-color: #ed1944;
    box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
  }

.filter-actions {
  display: flex;
  gap: 10px;
}

.slider-wrap {
  padding: 0px 5px;
}

  .slider-title {
      display: flex;
      font-size: .8em;
      font-weight: bold;
  }

  .slider-value {
    color: #ed1944;
    font-weight: bold;
    margin: 0 3px;
  }

  .custom-slider :deep(.el-slider__bar) {
    height: 6px;
    background-color: rgba(237, 25, 68, 1);
  }

  .custom-slider :deep(.el-slider__button) {
    width: 18px;
    height: 18px;
    border: 2px solid white;
    background-color: rgba(237, 25, 68, 1);
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
  }

  .search-btn, .reset-btn {
    flex: 1;
    padding: 8px 16px;
    border: none;
    border-radius: 4px;
    font-size: 14px;
    cursor: pointer;
    transition: background-color 0.2s;
  }

  .search-btn {
    background: #ed1944;
    color: white;
  }

    .search-btn:hover:not(:disabled) {
      background: #d0103a;
    }

.search-btn:disabled {
  background: #6c757d;
  cursor: not-allowed;
}

.reset-btn {
  background: #6c757d;
  color: white;
}

.reset-btn:hover:not(:disabled) {
  background: #545b62;
}

.reset-btn:disabled {
  background: #adb5bd;
  cursor: not-allowed;
}

@media (max-width: 768px) {
  .filters-container {
    grid-template-columns: 1fr;
  }

  .filter-actions {
    display: flex;
    justify-content: stretch;
    gap:10px;
  }
  
  .search-btn, .reset-btn {
    flex: 1;
  }
}
</style>
