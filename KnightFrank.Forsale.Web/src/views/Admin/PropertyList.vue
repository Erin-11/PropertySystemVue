<template>
  <el-container style="padding: 20px">
    <el-header>
      <h1>Property Management</h1>
      <el-button type="primary" @click="showForm()">Add Property</el-button>
    </el-header>

    <el-main>
      <el-table :data="properties" border stripe style="width: 100%">
        <el-table-column prop="region" label="Region"></el-table-column>
        <el-table-column prop="district" label="District"></el-table-column>
        <el-table-column prop="propertyType" label="PropertyType"></el-table-column>
        <el-table-column prop="salePrice" label="SalePrice" align="right">
          <template #default="{ row }">
            HK$ {{ row.salePrice.toLocaleString() }}
          </template>
        </el-table-column>
        <el-table-column label="operation" width="150">
          <template #default="{ row }">
            <el-button size="small" @click="editProperty(row)">edit</el-button>
            <el-button size="small" type="danger" @click="deleteProperty(row.id)">delete</el-button>
          </template>
        </el-table-column>
      </el-table>

      <PropertyForm v-if="isFormVisible"
                    :selected-property="selectedProperty"
                    @close="isFormVisible = false"
                    @saved="refreshList" />
    </el-main>
  </el-container>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import PropertyForm from './PropertyForm.vue';
  import { propertyService }  from '@/services/propertyService';

interface Property {
  id?: number;
  region: string;
  district: string;
  propertyType: string;
  salePrice: number;
  address: string;
  addressTC: string;
  grossArea: string;
  saleableArea: string;
  yearBuilt: string;
  refNo: string;
  kfRefNo: string;
  listedDate: Date;
  imageUrl: string;
}

const properties = ref<Property[]>([]);
const isFormVisible = ref(false);
const selectedProperty = ref<Property | null>(null);

const refreshList = async () => {
  properties.value = await propertyService.getProperties();
};

const showForm = () => {
  selectedProperty.value = null;
  isFormVisible.value = true;
};

const editProperty = (item: Property) => {
  selectedProperty.value = item;
  isFormVisible.value = true;
};

const deleteProperty = async (id: number) => {
  try {
    await propertyService.deleteProperty(id);
    refreshList();
  } catch (err) {
    alert('Failed to delete property. Please try again.');
  }
};

onMounted(async () => {
  refreshList();
});
</script>
