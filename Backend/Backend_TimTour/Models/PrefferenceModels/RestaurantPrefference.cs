namespace Backend_TimTour.Models.PrefferenceModels
{
    public class RestaurantPrefference
    {
        public string CusineTypes { get; set; }
        public string Atmosphere {  get; set; }

        public int PriceRange { get; set; }
        public string SpecialFeatures { get; set; }
        public string DietaryRestrictions { get; set; }
    }
}
