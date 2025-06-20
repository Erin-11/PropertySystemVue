import axios from 'axios'
import type { Property, PropertySearchRequest, PagedResult, FilterOptions } from '@/types/property'
import mockData from '@/mock/properties.json'

const useMock = true
// 模拟延迟
function delay<T>(data: T): Promise<T> {
  return new Promise(resolve => {
    setTimeout(() => resolve(data), 500)
  })
}

const API_BASE_URL = 'http://localhost:12001/api'


const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
})

export const propertyService = {
  async searchProperties(searchRequest: PropertySearchRequest): Promise<PagedResult<Property>> {
    if (useMock) {
      // 过滤 mock 数据
      let filtered = [...mockData.data]

      if (searchRequest.region) {
        filtered = filtered.filter(p => p.region === searchRequest.region)
      }

      if (searchRequest.district) {
        filtered = filtered.filter(p => p.district === searchRequest.district)
      }

      if (searchRequest.propertyTypes?.length) {
        filtered = filtered.filter(p =>
          searchRequest.propertyTypes!.includes(p.propertyType)
        )
      }

      if (searchRequest.minPrice !== undefined) {
        filtered = filtered.filter(p => p.salePrice >= searchRequest.minPrice!)
      }

      if (searchRequest.maxPrice !== undefined) {
        filtered = filtered.filter(p => p.salePrice <= searchRequest.maxPrice!)
      }

      // 分页处理
      const totalCount = filtered.length
      const totalPages = Math.ceil(totalCount / searchRequest.pageSize)
      const start = (searchRequest.page - 1) * searchRequest.pageSize
      const end = start + searchRequest.pageSize
      const data = filtered.slice(start, end)

      return delay({
        data,
        totalCount,
        totalPages,
        hasNextPage: searchRequest.page < totalPages,
        hasPreviousPage: searchRequest.page > 1,
        page: searchRequest.page,
        pageSize: searchRequest.pageSize
      })
    } else {
      const response = await api.post<PagedResult<Property>>('/properties/search', searchRequest)
      return response.data
    }
  },

  async getFilterOptions(): Promise<FilterOptions> {
    if (useMock) {
      return delay({
        regions: ['Hong Kong Island', 'Kowloon', 'New Territories'],
        districts: ['Tsim Sha Tsui', 'Shatin', 'Chai Wan', 'Fanling', 'Sham Shui Po', 'Mong Kok',
          'Kwun Tong', 'Tsuen Wan', 'Tin Shui Wai', 'Kwai Chung', 'Yuen Long' ],
        propertyTypes: ['Office', 'Residential (HOS)', 'Village', 'Commercial', 'Industrial', 'Residential', 'Car Park'],
        priceRanges: []
      })
    } else {
      const response = await api.get<FilterOptions>('/properties/filters')
      return response.data
    }
  },

  async getProperty(propertyId: number): Promise<Property> {
    if (useMock) {
      const item = mockData.data.find(p => p.propertyId === propertyId)
      if (!item) throw new Error('Property not found')
      return delay(item)
    } else {
      const response = await api.get<Property>(`/properties/${propertyId}`)
      return response.data
    }
  }
}
