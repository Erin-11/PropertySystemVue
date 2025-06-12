<template>
  <div class="property-detail-container">
    <!-- Loading State -->
    <div v-if="loading" class="loading-state">
      <div class="loading-spinner"></div>
      <p>Loading property details...</p>

    </div>

    <!-- Error State -->
    <div v-else-if="error" class="error-state">
      <p>{{ error }}</p>
      <button @click="loadProperty" class="retry-btn">
        Try Again

      </button>

    </div>

    <!-- Property Details -->
    <div v-else-if="property" class="property-detail">
      <!-- Header with back button -->
      <div class="detail-header">
        <span @click="goBack" class="back-btn">
          Home
        </span>
        <span> > </span>
        <span>Property Details</span>
      </div>

      <div class="property-main-layout">
        <div class="left-section">
          <!-- Property Image -->
          <div class="property-image-section">
            <img :src="property.imageUrl || '/placeholder-property.jpg'"
                 :alt="property.address"
                 @error="onImageError"
                 class="property-main-image" />
            <div class="property-type-badge">{{ property.propertyType }}</div>
          </div>

          <!-- Property Information -->
          <div class="property-info">
            <div class="property-main-info">
              <div class="address-info" @click="openInMap(property.address)">
                <span class="address-icon">📍</span>
                <span class="address-text">{{ property.address }}</span>
              </div>
              <div class="property-location">
                <span class="region">{{ property.region }}</span>
                <span class="separator">•</span>
                <span class="district">{{ property.district }}</span>

              </div>
              <div class="property-price">${{ formatPrice(property.salePrice) }}</div>

            </div>

            <!-- Basic Details -->
            <div class="property-basic-details">
              <h2>Basic Information</h2>
              <div class="details-grid">
                <div class="detail-item">
                  <span class="detail-icon">🛏️</span>
                  <div class="detail-content">
                    <span class="detail-label">Bedrooms</span>
                    <span class="detail-value">{{ property.bedrooms }}</span>

                  </div>

                </div>
                <div class="detail-item">
                  <span class="detail-icon">🚿</span>
                  <div class="detail-content">
                    <span class="detail-label">Bathrooms</span>
                    <span class="detail-value">{{ property.bathrooms }}</span>

                  </div>

                </div>
                <div class="detail-item">
                  <span class="detail-icon">📐</span>
                  <div class="detail-content">
                    <span class="detail-label">Area</span>
                    <span class="detail-value">{{ property.area }} m²</span>

                  </div>

                </div>

              </div>

            </div>

            <!-- Detailed Information -->
            <div class="property-detailed-info">
              <h2>Detailed Information</h2>
              <div class="details-grid">
                <div class="detail-item">
                  <div class="detail-content">
                    <span class="detail-label">Gross Area</span>
                    <span class="detail-value">{{ property.grossArea || 'N/A' }}</span>

                  </div>

                </div>
                <div class="detail-item">
                  <div class="detail-content">
                    <span class="detail-label">Saleable Area</span>
                    <span class="detail-value">{{ property.saleableArea || 'N/A' }}</span>

                  </div>

                </div>
                <div class="detail-item">
                  <div class="detail-content">
                    <span class="detail-label">Year Built</span>
                    <span class="detail-value">{{ property.yearBuilt || 'N/A' }}</span>

                  </div>

                </div>
                <div class="detail-item">
                  <div class="detail-content">
                    <span class="detail-label">Reference No</span>
                    <span class="detail-value">{{ property.refNo || 'N/A' }}</span>

                  </div>

                </div>

              </div>

            </div>

            <!-- Description -->
            <div class="property-description-section" v-if="property.description">
              <h2>Description</h2>
              <p class="property-description">{{ property.description }}</p>

            </div>

            <!-- Listing Information -->
            <div class="property-listing-info">
              <h2>Listing Information</h2>
              <div class="listing-details">
                <div class="detail-item">
                  <div class="detail-content">
                    <span class="detail-label">Listed Date</span>
                    <span class="detail-value">{{ formatDate(property.listedDate) }}</span>

                  </div>

                </div>

              </div>

            </div>

          </div>
        </div>

        <div class="right-section">
          <div class="contact-card">
            <div class="contact-header">
              <h4>Contact Us</h4>
              <div class="contact-header-text">
                For enquiry, please contact Asset Management & Investment Services Division
                如有垂詢,歡迎賜電資產管理及投資服務部
              </div>
            </div>
            <div>
              <h3>Enquiry Hotline (查詢熱線) +852 2846 9577</h3>
              <div class="contact-info">
                <div class="contact-item">
                  <strong>Name:</strong> Gigi Yeung (EAA Lic No E-112058)
                </div>
                <div class="contact-item">
                  <strong>Phone:</strong> +852 2846 9572
                </div>
                <div class="contact-item">
                  <strong>Email:</strong> gigi.yeung@hk.knightfrank.com
                </div>
              </div>

              <div class="contact-info">
                <div class="contact-item">
                  <strong>Name:</strong> Ranson Ip (EAA Lic No E-112034)
                </div>
                <div class="contact-item">
                  <strong>Phone:</strong> +852 2846 9573
                </div>
                <div class="contact-item">
                  <strong>Email:</strong> ranson.ip@hk.knightfrank.com
                </div>
              </div>
            </div>

            <div class="property-share">
              <button class="property-share-btn" type="submit" @click.prevent="showShareModal = true">Share Property</button>

              <div v-if="showShareModal" class="modal-overlay" @click.self="showShareModal = false">
                <div class="modal-content">
                  <div class="modal-header">
                    <h3>Share Property</h3>
                    <button class="close-btn" @click="showShareModal = false">×</button>
                  </div>
                  <div class="social-modal-body">
                    <a class="share-option" @click="generateWeChatQR" href="#">
                      <img alt="wechat" src="@/assets/icons/icon-wechat.png" />
                      <span>WeChat</span>
                    </a>
                    <a class="share-option" @click="shareToFacebook" href="#">
                      <img alt="facebook" src="@/assets/icons/icon-facebook.png" />
                      <span>Facebook</span>
                    </a>
                    <a class="share-option" @click="shareToTwitter" href="#">
                      <img alt="twitter" src="@/assets/icons/icon-twitter.png" />
                      <span>Twitter</span>
                    </a>
                    <a class="share-option" @click="shareToPinterest" href="#">
                      <img alt="pinterest" src="@/assets/icons/icon-pinterest.webp" />
                      <span>Pinterest</span>
                    </a>
                    <a class="share-option" @click="shareToWhatsapp" href="#">
                      <img alt="whatsapp" src="@/assets/icons/icon-whatsapp.webp" />
                      <span>Whatsapp</span>
                    </a>
                    <a class="share-option" @click="copyUrl" href="#">
                      <img alt="Url" src="@/assets/icons/icon-url.webp" />
                      <span>Url</span>
                    </a>
                  </div>
                </div>
              </div>
              <div v-if="showWechatQR" class="modal-overlay" @click.self="closeWechatQR">
                <div class="modal-content qr-modal">
                  <div class="modal-header">
                    <h3>Scan to Share</h3>
                    <button class="close-btn" @click="closeWechatQR">×</button>
                  </div>
                  <div class="wechat-modal-body qr-code-container">
                    <img :src="wechatQRUrl" alt="WeChat QR Code" />
                    <p>Please scan the code with wechat to share this property</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, computed } from 'vue'
  import { useRoute, useRouter } from 'vue-router'
  import type { Property } from '@/types/property'
  import { propertyService } from '@/services/propertyService'
  import QRCode from 'qrcode'

  const route = useRoute()
  const router = useRouter()

  const property = ref<Property | null>(null)
  const loading = ref(false)
  const error = ref<string | null>(null)

  const propertyId = route.params.id as string

  onMounted(() => {
    loadProperty()
  })

  async function loadProperty() {
    loading.value = true
    error.value = null

    try {
      const response = await propertyService.getProperty(parseInt(propertyId))
      property.value = response
    } catch (err) {
      error.value = 'Failed to load property details. Please try again.'
      console.error('Error loading property:', err)
    } finally {
      loading.value = false
    }
  }

  function formatPrice(price: number): string {
    return price.toLocaleString()
  }

  function formatDate(dateString: string): string {
    const date = new Date(dateString)
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    })
  }

  function onImageError(event: Event) {
    const img = event.target as HTMLImageElement
    img.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiBmaWxsPSIjZGRkIi8+CiAgPHRleHQgeD0iNTAlIiB5PSI1MCUiIGZvbnQtZmFtaWx5PSJBcmlhbCwgc2Fucy1zZXJpZiIgZm9udC1zaXplPSIxOCIgZmlsbD0iIzk5OSIgdGV4dC1hbmNob3I9Im1pZGRsZSIgZHk9Ii4zZW0iPk5vIEltYWdlPC90ZXh0Pgo8L3N2Zz4K'
  }

  function openInMap(address: string) {
    if (!address) return

    if (property.value?.latitude && property.value?.longitude) {
      const url = `https://www.google.com/maps?q=${property.value.latitude},${property.value.longitude}`
      window.open(url, '_blank')
      return
    }

    const encodedAddress = encodeURIComponent(address)
    window.open(`https://www.google.com/maps/search/?api=1&query=${encodedAddress}`, '_blank')
  }

  const showShareModal = ref(false)
  const showWechatQR = ref(false)
  const wechatQRUrl = ref('')
  const currentUrl = computed(() => {
    return `${window.location.origin}/propertyDetails/${property.value?.id}`;
  });  

  function generateWeChatQR() {
    QRCode.toDataURL(currentUrl.value)
      .then(dataUrl => {
        wechatQRUrl.value = dataUrl
        showWechatQR.value = true
      })
      .catch(err => {
        console.error('Failed to generate the QR code:', err)
        alert('Failed to generate the QR code.')
      })
  }

  function closeWechatQR() {
    showWechatQR.value = false
  }

  function shareToFacebook() {
    const url = encodeURIComponent(currentUrl.value)
    window.open(`https://www.facebook.com/sharer/sharer.php?u=${url}`, '_blank')
  }

  function shareToTwitter() {
    const url = encodeURIComponent(currentUrl.value)
    const text = encodeURIComponent(`Please check out this property: ${property.value?.address}`)
    window.open(`https://twitter.com/intent/tweet?url=${url}&text=${text}`, '_blank')
  }

  function shareToPinterest() {
    const media = encodeURIComponent(property.value?.imageUrl || '') 
    const description = encodeURIComponent(`Property: ${property.value?.address} - Price: $${property.value?.salePrice}`)
    const url = encodeURIComponent(currentUrl.value)

    if (!media) {
      alert('This property has no image to share on Pinterest.')
      return
    }

    window.open(`https://pinterest.com/pin/create/button/?url=${url}&media=${media}&description=${description}`, '_blank')
  }

  function shareToWhatsapp() {
    const text = encodeURIComponent(`I found a great property: ${property.value?.address}\nPrice: $${property.value?.salePrice}\n\n${currentUrl.value}`)
    window.open(`https://api.whatsapp.com/send?text=${text}`, '_blank')
  }

  async function copyUrl() {
    try {
      await navigator.clipboard.writeText(currentUrl.value);
      alert('The link has been copied to the clipboard.');
    } catch (err) {
      console.error('Failed to copy link:', err);
      alert('Failed to copy link.Please copy it manually.');
    }
  }


  function goBack() {
    router.push('/')
  }
</script>


<style scoped>
  .property-detail-container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
  }


  .loading-state {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 60px 20px;
    color: #666;
  }


  .loading-spinner {
    width: 40px;
    height: 40px;
    border: 4px solid #f3f3f3;
    border-top: 4px solid #ed1944;
    border-radius: 50%;
    animation: spin 1s linear infinite;
    margin-bottom: 16px;
  }


  @keyframes spin {
    0% {
      transform: rotate(0deg);
    }

    100% {
      transform: rotate(360deg);
    }
  }


  .error-state {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 60px 20px;
    color: #dc3545;
  }

  .property-main-layout {
    display: flex;
    gap: 40px;
    margin-top: 20px;
  }

  .left-section {
    flex: 2;
  }

  .right-section {
    flex: 1;
  }

  .address-info {
    font-size: 16px;
    margin: 10px 0 20px;
    cursor: pointer;
  }

    .address-info:hover {
      color: #ed1944;
    }

  .adress-icon {
    margin-right: 8px;
  }

  .address-text {
  }

  .contact-card {
    border: 1px solid #ECECEC;
    border-radius: 16px;
    background: #fff;
    padding: 30px 20px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  }

    .contact-card h4 {
      margin-top: 0;
      font-size: 14px;
      color: #d0103a;
      font-weight: bold;
    }

  .contact-header-text {
    margin-top: 0;
    font-size: 14px;
    margin-bottom: 35px;
  }


  .contact-card h3 {
    font-size: 18px;
  }

  .contact-info {
    margin-top: 25px;
  }

    .contact-info .contact-item {
      margin-bottom: 10px;
    }

  .property-share {
    display: flex;
    justify-content: center;
    margin-top: 40px;
  }

    .property-share-btn {
      font-size: 16px;
      font-weight: 600;
      border-radius: 16px;
      width: 100%;
      max-width: 200px;
      padding: 12px 20px;
      background-color: #ed1944;
      color: white;
      border: none;
      cursor: pointer;
      transition: background-color 0.3s ease;
    }

      .property-share-btn:hover {
        background-color: #d0103a;
      }

  .qr-modal .qr-code-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    text-align: center;
  }

    .qr-modal .qr-code-container img {
      width: 200px;
      height: 200px;
      margin-bottom: 15px;
    }

  .qr-modal p {
    font-size: 14px;
    color: #666;
  }

  .retry-btn {
    margin-top: 16px;
    padding: 8px 16px;
    background: #ed1944;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }


    .retry-btn:hover {
      background: #d0103a;
    }


  .detail-header {
    margin-bottom: 20px;
    font-size: 16px;
  }


  .back-btn {
    font-weight: bold;
    color: rgba(237, 25, 68, 0.9);
    cursor: pointer;
  }


  .property-image-section {
    position: relative;
    margin-bottom: 30px;
    border-radius: 8px;
    overflow: hidden;
  }


  .property-main-image {
    width: 100%;
    height: 400px;
    object-fit: cover;
  }


  .property-type-badge {
    position: absolute;
    top: 20px;
    right: 20px;
    background: rgba(237, 25, 68, 0.9);
    color: white;
    padding: 8px 16px;
    border-radius: 6px;
    font-size: 14px;
    font-weight: 600;
  }


  .property-info {
    background: white;
    border-radius: 8px;
    padding: 30px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  }


  .property-main-info {
    margin-bottom: 40px;
    padding-bottom: 30px;
    border-bottom: 2px solid #f8f9fa;
  }


  .property-address {
    font-size: 28px;
    font-weight: 700;
    margin: 0 0 10px 0;
    color: #333;
  }


  .property-location {
    color: #666;
    font-size: 16px;
    margin-bottom: 15px;
  }


  .separator {
    margin: 0 10px;
  }


  .property-price {
    font-size: 32px;
    font-weight: 700;
    color: #ed1944;
  }


  .property-basic-details,
  .property-detailed-info,
  .property-description-section,
  .property-listing-info {
    margin-bottom: 40px;
  }


    .property-basic-details h2,
    .property-detailed-info h2,
    .property-description-section h2,
    .property-listing-info h2 {
      font-size: 20px;
      font-weight: 600;
      margin: 0 0 20px 0;
      color: #333;
      border-bottom: 2px solid #ed1944;
      padding-bottom: 8px;
    }


  .details-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 20px;
  }


  .detail-item {
    display: flex;
    align-items: center;
    padding: 15px;
    background: #f8f9fa;
    border-radius: 6px;
    border-left: 4px solid #ed1944;
  }


  .detail-icon {
    font-size: 20px;
    margin-right: 12px;
  }


  .detail-content {
    display: flex;
    flex-direction: column;
    flex: 1;
  }


  .detail-label {
    font-size: 12px;
    color: #666;
    font-weight: 600;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    margin-bottom: 4px;
  }


  .detail-value {
    font-size: 16px;
    color: #333;
    font-weight: 600;
  }


  .property-description {
    font-size: 16px;
    line-height: 1.6;
    color: #555;
    margin: 0;
  }


  .listing-details .detail-item {
    max-width: 300px;
  }


  @media (max-width: 768px) {
    .property-detail-container {
      padding: 10px;
    }


    .property-main-image {
      height: 250px;
    }


    .property-info {
      padding: 20px;
    }


    .property-address {
      font-size: 22px;
    }


    .property-price {
      font-size: 24px;
    }


    .details-grid {
      grid-template-columns: 1fr;
      gap: 15px;
    }


    .detail-item {
      padding: 12px;
    }
  }

  @media (max-width: 768px) {
    .property-main-layout {
      flex-direction: column;
    }

    .left-section,
    .right-section {
      width: 100%;
    }

    .property-main-image {
      height: 250px;
    }

    .property-info {
      padding: 20px;
    }

    .property-price {
      font-size: 24px;
    }

    .property-address {
      font-size: 22px;
    }

    .address-info {
      font-size: 14px;
    }

    .details-grid {
      grid-template-columns: 1fr;
      gap: 15px;
    }

    .detail-item {
      padding: 12px;
    }

    .contact-card {
      margin-top: 20px;
    }

  }

  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }

  /* Modal Content */
  .modal-content {
    background: white;
    border-radius: 8px;
    width: 300px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
    position: relative;
  }

  .modal-header {
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
    padding: 10px 0;
    background-color: #ed1944;
  }

    .modal-header h3 {
      margin: 0;
      font-size: 18px;
      color: #fff;
    }

  .close-btn {
    position: absolute;
    right: 5px;
    font-size: 24px;
    background: none;
    border: none;
    cursor: pointer;
    color: #fff;
  }
  .social-modal-body {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    padding: 20px 10px;
  }
  .wechat-modal-body {
    display: flex;
    flex-direction: column;
    gap: 15px;
    padding: 20px;
  }

  .share-option {
    display: inline-block;
    gap: 10px;
    padding: 10px 10px;
    cursor: pointer;
    text-decoration: none;
    width: 75px;
    text-align: center;
    color: #333;
    transition: color 0.3s ease;
  }

    .share-option:hover,
    .share-option:active {
      color: #ed1944;
      background-color: #fff;
    }

    .share-option img {
      width: 50px;
      border: 0;
      height: auto;
      vertical-align: middle;
    }

  .share-option span {
    font-size: 12px;
    padding-top: 2px;
  }
</style>
