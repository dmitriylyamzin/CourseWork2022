using System;
using System.Collections.Generic;
using System.Text;

namespace SC.Controllers
{
    public class CurrentSymptoms
    {
        public List<int> symptomsList;
        public List<double> symptomsString = new List<double>();

        public CurrentSymptoms(List<int> _symptomsList)
        {
            symptomsList = _symptomsList;
        }

        public void RecognizeSymptoms()
        {
            for (int i = 0; i < 226; i++)
            {
                symptomsString.Add(0);
            }
            for (int i = 0; i < symptomsList.Count; i++)
            {
                symptomsString[symptomsList[i]] = 1;
            }
        }

        public void RecognizeTrueSymptom(int index)
        {
                symptomsString[index - 1] = 1;
        }

        public void RecognizeProbableSymptom(int index)
        {
            symptomsString[index - 1] = 0.5;
        }

        public void RecognizeNegativeSymptom(int index)
        {
            symptomsString[index - 1] = -1;
        }
    }
}
