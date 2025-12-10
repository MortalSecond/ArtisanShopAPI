namespace ArtisanShopAPI.Services
{
    public interface IFormattingService
    {
        string BuildFeatureList(string request);
        string BuildTreatmentList(string request);
        string FormatSize(string size);
        string FormatStoneCoverage(string coverage);
        string FormatFrame(string frame);
        string FormatShipping(string shipping);
        string FormatFeatureName(string feature);
    }
    public class FormattingService : IFormattingService
    {
        // Helper methods to build HTML-compatible strings from checkboxes
        public string BuildFeatureList(string features)
        {
            string featuresList = string.Empty;
            if (!string.IsNullOrEmpty(features))
            {
                string[] featuresSplit = features.Split(',');
                featuresList = "<ul>" + string.Join("", featuresSplit.Select(f => $"<li>{FormatFeatureName(f)}</li>"));
                return featuresList;
            }
            else
                return "No special features requested.";
        }

        public string BuildTreatmentList(string treatments)
        {
            string treatmentsList = string.Empty;
            if (!string.IsNullOrEmpty(treatments))
            {
                string[] treatmentsSplit = treatments.Split(',');
                treatmentsList = "<ul>" + string.Join("", treatmentsSplit.Select(t => $"<li>{FormatFeatureName(t)}</li>"));
                return treatmentsList;
            }
            else
                return "No surface treatments requested.";
        }

        // Helper methods to format the painting information
        public string FormatSize(string size)
        {
            return size switch
            {
                "small" => "Small (up to 40cm)",
                "medium" => "Medium (60-120cm)",
                "large" => "Large (120-180cm)",
                "extra-large" => "Extra Large (180cm+)",
                _ => size
            };
        }

        public string FormatStoneCoverage(string coverage)
        {
            return coverage switch
            {
                "none" => "None",
                "light" => "Light Coverage (Accents)",
                "medium" => "Medium Coverage (Negative Space)",
                "heavy" => "Heavy Coverage (Margin and Negative Space)",
                _ => coverage
            };
        }

        public string FormatFrame(string frame)
        {
            return frame switch
            {
                "wooden" => "Wooden Frame",
                "mirror" => "Mirror Frame",
                "led-wooden" => "LED Wooden Frame",
                "led-mirror" => "LED Mirror Frame",
                _ => frame
            };
        }

        public string FormatShipping(string shipping)
        {
            return shipping switch
            {
                "domestic" => "Domestic (Mexico)",
                "north-america" => "North America",
                "international" => "International",
                _ => shipping
            };
        }

        public string FormatFeatureName(string feature)
        {
            return feature switch
            {
                "clock" => "Clock Mechanism",
                "diamond-strips" => "Diamond Strips",
                "studs" => "Studs in Picture",
                "leds" => "LEDs in Picture",
                "resin" => "Epoxy Resin Coating",
                "geode" => "Geode Negative Space",
                _ => feature
            };
        }
    }
}
