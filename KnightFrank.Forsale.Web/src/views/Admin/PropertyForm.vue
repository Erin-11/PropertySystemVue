<template>
  <el-dialog v-model="visible" title="Property Information" width="60%" @close="$emit('close')">
    <el-form ref="formRef" :model="form" label-width="120px" :rules="rules">
      <el-row :gutter="20">
        <el-col :span="12">
          <el-form-item label="Region" prop="region">
            <el-input v-model="form.region" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="District" prop="district">
            <el-input v-model="form.district" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="PropertyType" prop="propertyType">
            <el-input v-model="form.propertyType" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="SalePrice" prop="salePrice">
            <el-input-number v-model="form.salePrice" :step="1000" />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="Address" prop="address">
            <el-input v-model="form.address" />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="AddressCN" prop="addressTC">
            <el-input v-model="form.addressTC" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="GrossArea" prop="grossArea">
            <el-input v-model="form.grossArea" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="SaleableArea" prop="saleableArea">
            <el-input v-model="form.saleableArea" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="YearBuilt" prop="yearBuilt">
            <el-input v-model="form.yearBuilt" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="RefNo" prop="refNo">
            <el-input v-model="form.refNo" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="KF Ref No" prop="kfRefNo">
            <el-input v-model="form.kfRefNo" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="Listed Date" prop="listedDate">
            <el-date-picker v-model="form.listedDate" type="date" value-format="YYYY-MM-DD" />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="Image Url" prop="imageUrl">
            <el-input v-model="form.imageUrl" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-form-item>
        <el-button type="primary" @click="save">save</el-button>
        <el-button @click="$emit('close')">cancel</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
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
  listedDate: string;
  imageUrl: string;
}

const props = defineProps<{
  selectedProperty: Property | null;
}>();

const emit = defineEmits(['close', 'saved']);

const visible = ref(true);
const form = ref<Property>({
  region: '',
  district: '',
  propertyType: '',
  salePrice: 0,
  address: '',
  addressTC: '',
  grossArea: '',
  saleableArea: '',
  yearBuilt: '',
  refNo: '',
  kfRefNo: '',
  listedDate: new Date().toISOString().split('T')[0],
  imageUrl: ''
});

watch(() => props.selectedProperty, (newVal) => {
  if (newVal) {
    form.value = { ...newVal };
  }
}, { deep: true });

const rules = {
  region: [{ required: true, message: '请输入区域', trigger: 'blur' }],
};

const save = async () => {
  if (props.selectedProperty?.id) {
    await propertyService.updateProperty(props.selectedProperty.id, form.value);
  } else {
    await propertyService.addProperty(form.value);
  }
  emit('saved');
};
</script>
