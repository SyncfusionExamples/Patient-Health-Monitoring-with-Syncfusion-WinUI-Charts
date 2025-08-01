﻿
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace PatientHealthMonitoring
{
    public class ViewModel:INotifyPropertyChanged
    {
        private Random random;
        private double _time = 0;
        private double averageBP;
        private double averageSaturation;
        private double averageglucoseLevel;
        public ObservableCollection<ECGPoint> ECGData { get; set; } = new ObservableCollection<ECGPoint>();

        public ObservableCollection<PatientInfo> Patients { get; set; }
        public List<Brush> CustomPalettes { get; set; }

        private ObservableCollection<BloodVitalMetrics> bloodVitalMetrics = new();
        public ObservableCollection<BloodVitalMetrics> BloodVitalMetrics
        {
            get => bloodVitalMetrics;
            set
            {
                bloodVitalMetrics = value;
                OnPropertyChanged(nameof(BloodVitalMetrics));
            }
        }

        private ObservableCollection<MedicationAdherence> medication = new();
        public ObservableCollection<MedicationAdherence> Medications
        {
            get => medication;
            set
            {
                medication = value;
                OnPropertyChanged(nameof(Medications));
            }
        }

        private ObservableCollection<StepCount> stepCounts = new();
        public ObservableCollection<StepCount> StepCounts
        {
            get => stepCounts;
            set
            {
                stepCounts = value;
                OnPropertyChanged(nameof(StepCounts));
            }
        }

        private double bodyTemperature;
        public double BodyTemperature
        {
            get => bodyTemperature;
            set
            {
                bodyTemperature = value;
                OnPropertyChanged(nameof(BodyTemperature));
            }
        }

        private double bloodCholesterol;
        public double BloodCholesterol
        {
            get => bloodCholesterol;
            set
            {
                bloodCholesterol = value;
                OnPropertyChanged(nameof(BloodCholesterol));
            }
        }

        private double heartRate;
        public double HeartRate
        {
            get => heartRate;
            set
            {
                heartRate = value;
                OnPropertyChanged(nameof(HeartRate));
            }
        }

        private double respiratoryRate;
        public double RespiratoryRate
        {
            get => respiratoryRate;
            set
            {
                respiratoryRate = value;
                OnPropertyChanged(nameof(RespiratoryRate));
            }
        }

        private PatientInfo? _selectedPatient;
        public PatientInfo? SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if(value != null)
                {
                    _selectedPatient = value;
                    OnPropertyChanged(nameof(SelectedPatient));
                    DynamicUpdateData(SelectedPatient!);
                    SelectionChangedData(SelectedPatient!);
                }
            }
        }

        private ObservableCollection<HealthConditionInfo> healthConditionInfos = new();
        public ObservableCollection<HealthConditionInfo> HealthConditionInfos
        {
            get => healthConditionInfos;
            set
            {
                healthConditionInfos = value;
                OnPropertyChanged(nameof(HealthConditionInfos));
            }
        }

        private double minimum;
        public double Minimum
        {
            get => minimum;
            set
            {
                if(minimum != value)
                {
                    minimum = value;
                    OnPropertyChanged(nameof(Minimum));
                }
            }
        }

        private double maximum;
        public double Maximum
        {
            get => maximum;
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    OnPropertyChanged(nameof(Maximum));
                }
            }
        }

        private double interval;
        public double Interval
        {
            get => interval;
            set
            {
                if(interval != value)
                {
                    interval = value;
                    OnPropertyChanged(nameof(Interval));
                }
            }
        }

        private Visibility bpseriesVisibility;
        public Visibility BPSeriesVisibility
        {
            get => bpseriesVisibility;
            set
            {
                bpseriesVisibility = value;
                OnPropertyChanged(nameof(BPSeriesVisibility));
                if (BPSeriesVisibility == Visibility.Visible)
                    ShowBPSeriesInteraction = true;
                else
                    ShowBPSeriesInteraction = false;
            }
        }

        private Visibility oxygensaturationseriesVisibility;
        public Visibility OxygenSaturationSeriesVisibility
        {
            get => oxygensaturationseriesVisibility;
            set
            {
                oxygensaturationseriesVisibility = value;
                OnPropertyChanged(nameof(OxygenSaturationSeriesVisibility));
                if (OxygenSaturationSeriesVisibility == Visibility.Visible)
                    ShowOxygenSeriesInteraction = true;
                else
                    ShowOxygenSeriesInteraction = false;
            }
        }

        private Visibility glucoselevelseriesVisibility;
        public Visibility GlucoseLevelSeriesVisibility
        {
            get => glucoselevelseriesVisibility;
            set
            {
                glucoselevelseriesVisibility = value;
                OnPropertyChanged(nameof(GlucoseLevelSeriesVisibility));
                if (GlucoseLevelSeriesVisibility == Visibility.Visible)
                    ShowGlucoseSeriesInteraction = true;
                else
                    ShowGlucoseSeriesInteraction = false;
            }
        }

        private bool showbpseriesInteraction;
        public bool ShowBPSeriesInteraction
        {
            get=> showbpseriesInteraction;
            private set
            {
                showbpseriesInteraction = value;
                OnPropertyChanged(nameof(ShowBPSeriesInteraction));
            }
        }

        private bool showglucoseseriesInteraction;
        public bool ShowGlucoseSeriesInteraction
        {
            get => showglucoseseriesInteraction;
            private set
            {
                showglucoseseriesInteraction = value;
                OnPropertyChanged(nameof(ShowGlucoseSeriesInteraction));
            }
        }

        private bool showoxygenseriesInteraction;
        public bool ShowOxygenSeriesInteraction
        {
            get => showoxygenseriesInteraction;
            private set
            {
                showoxygenseriesInteraction = value;
                OnPropertyChanged(nameof(ShowOxygenSeriesInteraction));
            }
        }

        private object axisTitle = string.Empty;
        public object AxisTitle
        {
            get => axisTitle;
            set
            {
                if(axisTitle != value)
                {
                    axisTitle = value;
                    OnPropertyChanged(nameof(AxisTitle));
                }
            }
        }

        public ViewModel()
        {
            random = new Random();
            Minimum = 0;
            Maximum = 160;
            Interval = 40;

            Patients = GeneratePatientDetails();

            CustomPalettes = new List<Brush>
            {
                new SolidColorBrush(Color.FromArgb(255, 136, 84, 217)),
                new SolidColorBrush(Color.FromArgb(255, 255, 71, 126)),
                new SolidColorBrush(Color.FromArgb(255, 193, 82, 210)),
                new SolidColorBrush(Color.FromArgb(255, 205, 222, 31)),
                new SolidColorBrush(Color.FromArgb(255, 33, 150, 243)) 

            };

            BPSeriesVisibility = Visibility.Visible;
            OxygenSaturationSeriesVisibility = Visibility.Collapsed;
            GlucoseLevelSeriesVisibility = Visibility.Collapsed;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            timer.Tick += (s, e) =>
            {
                ECGData.Add(new ECGPoint { Time = _time, Voltage = GenerateECGWave(_time) });
                _time += 0.1;

                if (ECGData.Count > 50)
                    ECGData.RemoveAt(0);
            };
            timer.Start();

            var timer1 = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            timer1.Tick += (s, e) =>
            {
                DynamicUpdateData(SelectedPatient!);
            };
            timer1.Start();

            SelectedPatient = Patients[2];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<PatientInfo> GeneratePatientDetails()
        {
            return new ObservableCollection<PatientInfo>
            {
                new PatientInfo("P001", "Thomas", 30, "Male", 170, 70),
                new PatientInfo("P002", "Emily", 35, "Female", 160, 60),
                new PatientInfo("P003", "Grace", 28, "Female", 165, 58),
                new PatientInfo("P004", "Michael", 37, "Male", 175, 65),
                new PatientInfo("P005", "Sophia", 32, "Female", 163, 60),
                new PatientInfo("P006", "James", 42, "Male", 177, 72),
                new PatientInfo("P007", "Olivia", 29, "Female", 167, 62),
            };
        }

        private void SelectionChangedData(PatientInfo patient)
        {
            var patientActions = new Dictionary<string, Action>
            {
                { "Emily", () => SetPersonDetails(100, 108, 200, 240, 90, 120, 60, 80, 91, 94, 70, 99, 65, 35) },
                { "Grace", () => SetPersonDetails(97, 99, 100, 200, 125, 145, 85, 100, 95, 100, 101, 115, 75, 25) },
                { "Michael", () => SetPersonDetails(97, 99, 200, 240, 125, 145, 85, 100, 95, 100, 126, 136, 55, 45) },
                { "Sophia", () => SetPersonDetails(97, 99, 100, 200, 125, 145, 85, 100, 95, 100, 70, 99, 40, 60) },
                { "James", () => SetPersonDetails(100, 105, 200, 240, 90, 120, 60, 80, 81, 91, 100, 125, 75, 25) },
                { "Olivia", () => SetPersonDetails(97, 99, 100, 200, 125, 145, 85, 100, 95, 100, 70, 99, 50, 50) }
            };

            if (patientActions.TryGetValue(patient.Name, out var action))
                action();
            else
                SetPersonDetails(97, 99, 100, 200, 125, 145, 85, 100, 95, 100, 100, 125, 75, 25);
        }

        private void SetPersonDetails(int tempMin, int tempMax, int cholMin, int cholMax,
                                      int sysMin, int sysMax, int diaMin, int diaMax,
                                      int satMin, int satMax, int gluMin, int gluMax,
                                      int medTaken, int medMissed)
        {
            BodyTemperature = random.Next(tempMin, tempMax);
            BloodCholesterol = random.Next(cholMin, cholMax);

            BloodVitalMetrics = new ObservableCollection<BloodVitalMetrics>();
            string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            foreach (var day in days)
            {
                BloodVitalMetrics.Add(new BloodVitalMetrics(
                    day,
                    random.Next(sysMin, sysMax),
                    random.Next(diaMin, diaMax),
                    random.Next(satMin, satMax),
                    random.Next(gluMin, gluMax)
                ));
            }

            Medications = new ObservableCollection<MedicationAdherence>
            {
                new MedicationAdherence { Name = "Taken", Value = medTaken },
                new MedicationAdherence { Name = "Missed", Value = medMissed }
            };

            StepCounts = UpdateStepCountsData();

            double mapSum = 0, saturationSum = 0, glucoseSum = 0;

            foreach (var metric in BloodVitalMetrics)
            {
                double map = (metric.SystolicBP + (2 * metric.DiastolicBP)) / 3.0;
                mapSum += map;
                saturationSum += metric.OxygenSaturation;
                glucoseSum += metric.Glucoselevel;
            }

            averageBP = mapSum / BloodVitalMetrics.Count;
            averageSaturation = saturationSum / BloodVitalMetrics.Count;
            averageglucoseLevel = glucoseSum / BloodVitalMetrics.Count;

            HealthConditionInfos = UpdateHealthCondition();
        }

        private ObservableCollection<HealthConditionInfo> UpdateHealthCondition()
        {
            return new ObservableCollection<HealthConditionInfo>
            {
                new HealthConditionInfo("Body Temperature", BodyTemperature < 96.8 ? "Hypothermia" : (BodyTemperature >= 96.8 && BodyTemperature <= 99.5) ? "Normothermia" : "Fever"),
                new HealthConditionInfo("Blood Cholesterol", BloodCholesterol < 200 ? "Low Cholesterol" : (BloodCholesterol >= 200 && BloodCholesterol <= 240) ? "Medium Cholesterol" : "High Cholesterol"),
                new HealthConditionInfo("Blood Pressure", averageBP < 70 ? "Hypoperfusion" : (averageBP >= 70 && averageBP <= 100) ? "Normotension" : "Hypertension"),
                new HealthConditionInfo("Oxygen Saturation", (averageSaturation >= 91 && averageSaturation <= 94) ? "Border Line" : (averageSaturation >= 95 && averageSaturation <= 100) ? "Normoxemia" : "Hypoxemia"),
                new HealthConditionInfo("Glucose Level", (averageglucoseLevel >= 70 && averageglucoseLevel <= 99) ? "Normoglycemia" : (averageglucoseLevel >= 100 && averageglucoseLevel <= 125) ? "PreDiabetes" : "Diabetes"),
                new HealthConditionInfo("Heart Rate", HeartRate < 60 ? "Bradycardia" : (HeartRate >= 60 && HeartRate <= 100) ? "Normal" : "Tachycardia"),
                new HealthConditionInfo("Respiratory Rate", RespiratoryRate < 12 ? "Bradypnea" : (RespiratoryRate >= 12 && RespiratoryRate <= 20) ? "Eupnea" : "Tachypnea")
            };
        }

        private ObservableCollection<StepCount> UpdateStepCountsData()
        {
            return new ObservableCollection<StepCount>
            {
                new StepCount(){Day = "Sun", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Mon", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Tue", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Wed", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Thu", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Fri", Actual= random.Next(8000, 12000), Target = 10000},
                new StepCount(){Day = "Sat", Actual= random.Next(8000, 12000), Target = 10000},
            };
        }

        private double GenerateECGWave(double t)
        {
            double cycle = 0.6;
            double tMod = t % cycle;

            double p = Math.Exp(-Math.Pow((tMod - 0.1) / 0.02, 2)) * 0.2;
            double q = -Math.Exp(-Math.Pow((tMod - 0.2) / 0.008, 2)) * 0.15;
            double r = Math.Exp(-Math.Pow((tMod - 0.22) / 0.005, 2)) * 1.2;
            double s = -Math.Exp(-Math.Pow((tMod - 0.24) / 0.008, 2)) * 0.25;
            double tw = Math.Exp(-Math.Pow((tMod - 0.35) / 0.03, 2)) * 0.35;

            return p + q + r + s + tw;
        }

        private void DynamicUpdateData(PatientInfo patient)
        {
            var ranges = new Dictionary<string, (int heartMin, int heartMax, int respMin, int respMax)>
            {
                { "Emily", (49, 59, 12, 20) },
                { "Grace", (60, 100, 9, 11) },
                { "Michael", (101, 110, 12, 20) },
                { "Sophia", (60, 100, 12, 20) },
                { "James", (49, 59, 9, 11) },
                { "Olivia", (101, 110, 21, 25) }
            };

            var (heartMin, heartMax, respMin, respMax) =
                ranges.TryGetValue(patient.Name, out var range)
                ? range
                : (60, 100, 12, 20);

            HeartRate = random.Next(heartMin, heartMax);
            RespiratoryRate = random.Next(respMin, respMax);
        }
    }
}
