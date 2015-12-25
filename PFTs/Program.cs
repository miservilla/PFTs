using System;

namespace interpretingPFTs
{
    class Program
    {
        private static string PFT;
        private static string DLCO;
        static void Main(string[] args)
        {
            //This is a test for change.
            //This is a second test for change.
            //This is a third test for change.
            string pedsBronchodilatorPositive = "n";
            string adultBronchodilatorPositive = "n";
            double fvcLLN = 0;
            double fev1_fvcLLN = 0;
            double fev1_fvc = 0;
            double fev1PP = 0;
            double age = GetValue("Enter age: ");
            string smokingString = GetYesNoValue("Current or past smoking, Y or N? ");
            double fvc = GetValue("Enter FVC actual: ");
            double fvcPP = GetValue("Enter FVC percent predicted: ");
            if (age > 18)
            {
                fvcLLN = GetValue("Enter FVC LLN: ");
            }
            fev1PP = GetValue("Enter FEV1 percent predicted: ");
            if (age > 18)
            {
                fev1_fvc = GetValue("Enter FEV1/FVC actual: ");
            }
            double fev1_fvcPP = GetValue("Enter FEV1/FVC percent predicted: ");

            if (age > 18)
            {
                fev1_fvcLLN = GetValue("Enter FEV1/FVC LLN: ");
            }
            string bronchodilator = GetYesNoValue("Was bronchodilator therapy tried, Y or N? ");

            if (bronchodilator == "y")
            {
                if (age >= 5 && age <= 18)
                {
                    pedsBronchodilatorPositive = GetYesNoValue("Was there and increase in FEV1 or FVC > 12 %,\n" +
                    "or was there an increase in FVC > 80 % of predicted, Y or N? ");
                }
                if (age > 18)
                {
                    adultBronchodilatorPositive = GetYesNoValue("Was there an increase in FEV1 or FVC > 12 % and > 200 mL,\n" +
                    "or was there an increase in FVC > LLN of predicted, Y or N? ");
                }
            }
            string wasDLCO = GetYesNoValue("DLCO, Y or N? ");
            if (wasDLCO == "y")
            {
                double dlcoPredicted = GetValue("Enter DLCO predicted: ");
                double dlcoActual = GetValue("Enter DLCO actual: ");
                double dlcoLLN = GetValue("Enter DLCO LLN: ");
                double dlcoLLNPercent = (dlcoLLN / dlcoPredicted) * 100;
                double dlcoPercentPredicted = (dlcoActual / dlcoPredicted) * 100;
                Console.ForegroundColor = System.ConsoleColor.Red;
                if (dlcoPercentPredicted > 120)
                {
                    DLCO = "high";
                    Console.WriteLine("\nDLCO is " + DLCO + ".");
                }
                else if (dlcoPercentPredicted >= dlcoLLNPercent && dlcoPercentPredicted <= 120)
                {
                    DLCO = "normal";
                    Console.WriteLine("\nDLCO is " + DLCO + ".");
                }
                else if (dlcoPercentPredicted > 60 && dlcoPercentPredicted < dlcoLLNPercent)
                {
                    DLCO = "low mild";
                    Console.WriteLine("\nDLCO is " + DLCO + ".");
                }
                else if (dlcoPercentPredicted >= 40 && dlcoPercentPredicted <= 60)
                {
                    DLCO = "low moderate";
                    Console.WriteLine("\nDLCO is " + DLCO + ".");
                }
                else
                {
                    DLCO = "low severe";
                    Console.WriteLine("\nDLCO is " + DLCO + ".");
                }


                //DATA GATHERING ENDS AND LOGIC BEGINS HERE!

            }
            if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP < 80)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP > 79 && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                PFT = "od";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP > 84 && fvcPP < 80 && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nRestrictive pattern defect. \n");
                PFT = "rpd";
                Severity(fev1PP);
                Console.WriteLine("Confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP < 80 && bronchodilator == "y" && pedsBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed Pattern Defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, pure obstruction with air trapping is likely \n" +
                "chronic obstructive pulmonary disease.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP > 79 && bronchodilator == "y" && pedsBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator response, reversible obstruction (asthma).\n");
                PFT = "ro";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP > 79 && bronchodilator == "y" && pedsBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator resoponse, irreversible obstruction.\n");
                PFT = "io";
                Severity(fev1PP);
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP < 85 && fvcPP < 80 && bronchodilator == "y" && pedsBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 5 && age <= 18 && fev1_fvcPP > 84 && fvcPP > 79)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nPFTs are normal.");
                PFT = "n";
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP >= 70 && fvc >= fvcLLN)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nPFTs are normal. If there is still concern for asthma, order bronchoprovocation.");
                PFT = "n";
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP >= 70 && fvc < fvcLLN)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nRestrictive pattern defect. \n");
                PFT = "rpd";
                Severity(fev1PP);
                Console.WriteLine("Confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc < fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc < fvcLLN && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc < fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed Pattern Defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, pure obstruction with air trapping is likely \n" +
                "chronic obstructive pulmonary disease.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc >= fvcLLN && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                PFT = "od";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc >= fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator response, Reversible obstruction (asthma).\n");
                PFT = "ro";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age >= 65 && smokingString == "y" && fev1_fvcPP < 70 && fvc >= fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator response, irreversible obstruction.\n");
                PFT = "io";
                Severity(fev1PP);
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc >= fev1_fvcLLN && fvc >= fvcLLN)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nPFTs are normal. If there is still concern for asthma, order bronchoprovocation.");
                PFT = "n";
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc >= fev1_fvcLLN && fvc < fvcLLN)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nRestrictive pattern defect. \n");
                PFT = "rpd";
                Severity(fev1PP);
                Console.WriteLine("Confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc < fev1_fvcLLN && fvc < fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, confirm restrictive defect through full pulmonary function tests with DLCO.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc < fev1_fvcLLN && fvc < fvcLLN && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed pattern defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc < fev1_fvcLLN && fvc < fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nMixed Pattern Defect.\n");
                PFT = "mpd";
                Severity(fev1PP);
                Console.WriteLine("Based on bronchodilator response, pure obstruction with air trapping is likely \n" +
                "chronic obstructive pulmonary disease.\n");
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc < fev1_fvcLLN && fvc >= fvcLLN && bronchodilator == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                PFT = "od";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else if (age > 18 /*&& smokingString == "n"*/ && fev1_fvc < fev1_fvcLLN && fvc >= fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "y")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator response, reversible obstruction (asthma).\n");
                PFT = "ro";
                Severity(fev1PP);
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
            else //if (age > 18 && smokingString == "n" && fev1_fvc < fev1_fvcLLN && fvc >= fvcLLN && bronchodilator == "y" && adultBronchodilatorPositive == "n")
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\nObstructive defect.\n");
                Console.WriteLine("\nBased on bronchodilator response, irreversible obstruction.\n");
                PFT = "io";
                Severity(fev1PP);
                Console.WriteLine("Consider differential diagnosis:\n\n" +
                "α1 - antitrypsin deficiency \nAsthma \nBronchiectasis \n" +
                "Bronchiolitis obliterans \nChronic obstructive pulmonary disease \n" +
                "Cystic fibrosis \nSilicosis(early)");
                if (wasDLCO == "y")
                {
                    DLCODiagnosis(DLCO);
                }
                Console.ReadLine();
            }
        }


        //THIS IS WHERE THE LOGIC ENDS AND METHODS BEGIN!

        private static void DLCODiagnosisNum(object dLCONum)
        {
            throw new NotImplementedException();
        }

        private static double GetValue(string label)
        {
            //the value to be returned
            double value;

            //loop until you get a valid entry
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                if (Double.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Input error, try again!");
                }
            }
        }
        private static string GetYesNoValue(string label)
        {
            //the value to be returned
            string YesNo;

            //loop until you get a valid entry
            while (true)
            {
                Console.Write(label);
                YesNo = Console.ReadLine();
                YesNo = YesNo.ToLower();
                if (YesNo == "y")
                {
                    return YesNo;
                }
                else if (YesNo == "n")
                {
                    return YesNo;
                }
                else
                {
                    Console.WriteLine("Input error, try again!");
                }
            }
        }
        private static void Severity(double fev1PP)
        {
            if (fev1PP >= 70)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Mild.\n");
            }
            else if (fev1PP > 59 && fev1PP < 70)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Moderate.\n");
            }
            else if (fev1PP > 49 && fev1PP < 60)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Moderately severe.\n");
            }
            else if (fev1PP > 34 && fev1PP < 50)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Severe.\n");
            }
            else if (fev1PP < 35)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Very severe.\n");
            }
        }
        private static void DLCODiagnosis(string DLCO)
        {
            Console.Write("\nDifferential diagnosis based on DLCO is ");
            if (DLCO == "high")
            {
                Console.WriteLine("asthma, left-to-right intracardiac shunts, polycythemia, pulmonary hemorrhage.");
            }
            else if (DLCO == "normal")
            {
                if (PFT == "rp")
                {
                    Console.WriteLine("kyphoscoliosis, morbid obesity, neuromuscular weakness, pleural effusion.");
                }
                else
                {
                    Console.WriteLine("α1-antitrypsin deficiency, asthma, bronchiectasis, chronic bronchitis.");
                }
            }
            else if (DLCO == "low mild" || DLCO == "low moderate" || DLCO == "low severe")
            {
                if (PFT == "rpd")
                {
                    Console.WriteLine("asbestosis, berylliosis, hypersensitivity pneumonitis,\n" +
                    "idiopathic pulmonary fibrosis, Langerhans cell histiocytosis(histiocytosis X),\n" +
                    "lymphangitic spread of tumor, miliary tuberculosis, sarcoidosis, silicosis(late).");
                }
                else if (PFT == "od")
                {
                    Console.WriteLine("cystic fibrosis, emphysema, silicosis (early).");
                }
                else
                {
                    Console.WriteLine("chronic pulmonary emboli, congestive heart failure,\n" +
                    "connective tissue disease with pulmonary involvement, dermatomyositis/ polymyositis,\n" +
                    "inflammatory bowel disease, interstitial lung disease(early),\n" +
                    "primary pulmonary hypertension, rheumatoid arthritis, systemic lupus erythematosus,\n" +
                    "systemic sclerosis, Wegener granulomatosis (also called granulomatosis with polyangiitis).");
                }
            }
        }
    }


}
