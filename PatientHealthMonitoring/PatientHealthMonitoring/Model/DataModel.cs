
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace PatientHealthMonitoring
{
    public class PatientInfo
    {
        public string PatientID{ get; set; }
        public string Name{ get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public PatientInfo(string patientID, string name, int age, string gender, double height, double weight)
        {
            PatientID = patientID;
            Name = name;
            Age = age;
            Gender = gender;
            Height = height;
            Weight = weight;
        }
    }

    public class BloodVitalMetrics
    {
        public string Day { get; set; }
        public double SystolicBP { get; set; }
        public double DiastolicBP { get; set; }
        public double OxygenSaturation { get; set; }
        public double Glucoselevel { get; set; }

        public BloodVitalMetrics(string day, double systolicBP, double diastolicBP, double oxygenSaturation, double glucoselevel)
        {
            Day = day;
            SystolicBP = systolicBP;
            DiastolicBP = diastolicBP;
            OxygenSaturation = oxygenSaturation;
            Glucoselevel = glucoselevel;
        }
    }

    public class MedicationAdherence
    {
        public string? Name { get; set; }
        public double Value { get; set; }
    }

    public class StepCount
    {
        public string? Day { get; set; }
        public double Actual { get; set; }
        public double Target { get; set; }
    }

    public class ECGPoint
    {
        public double Time { get; set; }
        public double Voltage { get; set; }
    }

    public class HealthConditionInfo
    {
        public string? Metrics { get; set; }
        public string? ConditionName { get; set; }
        public double Status
        {
            get
            {
                return ConditionName switch
                {
                    "Hypothermia" or "Low Cholesterol" or "Hypoperfusion" or "Bradycardia" or "Bradypnea" => 25,
                    "Normothermia" or "Medium Cholesterol" or "Normotension" or "Normal" or "Eupnea" => 50,
                    "Fever" or "High Cholesterol" or "Hypertension" or "Tachycardia" or "Tachypnea" => 75,
                    "Hypoxemia" => 45,
                    "Border Line" => 65,
                    "Normoxemia" => 95,
                    "Normoglycemia" => 40,
                    "PreDiabetes" => 60,
                    "Diabetes" => 90,
                    _ => 0
                };
            }
        }
        public Brush GaugeColor
        {
            get
            {
                return ConditionName switch
                {
                    "Fever" or "High Cholesterol" or "Hypertension" or "Tachycardia" or "Tachypnea" or "Hypoxemia" or "Diabetes" => new SolidColorBrush(Color.FromArgb(255, 255, 93, 106)),
                    "Normothermia" or "Medium Cholesterol" or "Normotension" or "Normal" or "Eupnea" or "Normoxemia" or "Normoglycemia" => new SolidColorBrush(Color.FromArgb(255, 26, 201, 38)),
                    "Hypothermia" or "Low Cholesterol" or "Hypoperfusion" or "Bradycardia" or "Bradypnea" or "Border Line" or "PreDiabetes" => new SolidColorBrush(Color.FromArgb(255, 254, 194, 0)),
                    _ => new SolidColorBrush(Colors.Gray)
                };
            }
        }
        public HealthConditionInfo( string metrics, string conditionName)
        {
            Metrics = metrics;
            ConditionName = conditionName;
        }
    }

}
