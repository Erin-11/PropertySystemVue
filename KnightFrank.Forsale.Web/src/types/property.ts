export interface Property {
  propertyId: number
  region: string
  district: string
  propertyType: string
  salePrice: number
  address: string
  grossArea: string
  saleableArea: string
  yearBuilt: string
  refNo: string
  description: string
  bedrooms: number
  bathrooms: number
  area: number
  listedDate: string
  imageUrl: string
  latitude?: number;
  longitude?: number;
  mapUpdatedAt?: string; // ISO Date String
}

export interface PropertySearchRequest {
  region?: string
  district?: string
  propertyTypes?: string[]
  minPrice?: number
  maxPrice?: number
  page: number
  pageSize: number
}

export interface PagedResult<T> {
  data: T[]
  totalCount: number
  page: number
  pageSize: number
  totalPages: number
  hasNextPage: boolean
  hasPreviousPage: boolean
}

export interface FilterOptions {
  regions: string[]
  districts: string[]
  propertyTypes: string[]
  priceRanges: PriceRange[]
}

export interface PriceRange {
  label: string
  min: number
  max: number
}
