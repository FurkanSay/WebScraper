using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Trendyol.Models
{

    public class TrendyolProduct
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public object error { get; set; }
        public Result result { get; set; }
        public Headers headers { get; set; }
    }

    public class Result
    {
        public string slpName { get; set; }
        public Product[] products { get; set; }
        public int totalCount { get; set; }
        public int offset { get; set; }
        public string roughTotalCount { get; set; }
        public string searchStrategy { get; set; }
        public string title { get; set; }
        public string uxLayout { get; set; }
        public string queryTerm { get; set; }
        public int pageIndex { get; set; }
        public Widget[] widgets { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string[] images { get; set; }
        public string imageAlt { get; set; }
        public Brand brand { get; set; }
        public int tax { get; set; }
        public Ratingscore ratingScore { get; set; }
        public bool showSexualContent { get; set; }
        public int productGroupId { get; set; }
        public bool hasReviewPhoto { get; set; }
        public string cardType { get; set; }
        public Section[] sections { get; set; }
        public Variant[] variants { get; set; }
        public string categoryHierarchy { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string url { get; set; }
        public int merchantId { get; set; }
        public int campaignId { get; set; }
        public Price price { get; set; }
        public Promotion[] promotions { get; set; }
        public int rushDeliveryDuration { get; set; }
        public bool freeCargo { get; set; }
        public string campaignName { get; set; }
        public string listingId { get; set; }
        public string winnerVariant { get; set; }
        public int itemNumber { get; set; }
        public string discountedPriceInfo { get; set; }
        public bool hasVideoContent { get; set; }
        public bool hasCrossPromotion { get; set; }
        public bool hasCollectableCoupon { get; set; }
        public bool sameDayShipping { get; set; }
        public bool isLegalRequirementConfirmed { get; set; }
        public Badge[] badges { get; set; }
        public Stamp[] stamps { get; set; }
        public Advert advert { get; set; }
        public int collectableCouponDiscount { get; set; }
    }

    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Ratingscore
    {
        public float averageRating { get; set; }
        public int totalCount { get; set; }
    }

    public class Price
    {
        public float sellingPrice { get; set; }
        public float originalPrice { get; set; }
        public float discountedPrice { get; set; }
        public int buyingPrice { get; set; }
    }

    public class Advert
    {
        public string advertId { get; set; }
        public float sortingScore { get; set; }
        public float adScore { get; set; }
        public Adscores adScores { get; set; }
        public float cpc { get; set; }
        public float minCpc { get; set; }
        public float eCpc { get; set; }
        public float advertSlot { get; set; }
    }

    public class Adscores
    {
        public float _1 { get; set; }
        public float _2 { get; set; }
    }

    public class Section
    {
        public string id { get; set; }
    }

    public class Variant
    {
        public string attributeValue { get; set; }
        public string attributeName { get; set; }
        public Price1 price { get; set; }
        public string listingId { get; set; }
        public int campaignId { get; set; }
        public int merchantId { get; set; }
        public string discountedPriceInfo { get; set; }
        public bool hasCollectableCoupon { get; set; }
        public bool sameDayShipping { get; set; }
    }

    public class Price1
    {
        public float discountedPrice { get; set; }
        public float buyingPrice { get; set; }
        public float originalPrice { get; set; }
        public float sellingPrice { get; set; }
    }

    public class Promotion
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public DateTime promotionEndDate { get; set; }
    }

    public class Badge
    {
        public string title { get; set; }
        public string type { get; set; }
        public bool priority { get; set; }
    }

    public class Stamp
    {
        public string imageUrl { get; set; }
        public string type { get; set; }
        public string position { get; set; }
        public float aspectRatio { get; set; }
        public int priority { get; set; }
        public string tagType { get; set; }
    }

    public class Widget
    {
        public Configuration configuration { get; set; }
        public string type { get; set; }
        public string displayType { get; set; }
        public string feedingSource { get; set; }
        public string feedingSourceDataKey { get; set; }
        public int displayOrder { get; set; }
        public int widgetOrder { get; set; }
        public string title { get; set; }
        public string eventKey { get; set; }
    }

    public class Configuration
    {
        public int storefrontId { get; set; }
        public string culture { get; set; }
        public string widgetGwUrl { get; set; }
        public string accountGwUrl { get; set; }
        public string recoGwUrl { get; set; }
        public bool priceBoxV2Enabled { get; set; }
        public bool searchStoreAdsDisabled { get; set; }
        public string publicSfxWidgetGwUrl { get; set; }
    }

    public class Headers
    {
        public string tysidecarcachable { get; set; }
    }

}
