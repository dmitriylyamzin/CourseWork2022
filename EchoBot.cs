using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
namespace SC.Bots
{
    public class EchoBot : ActivityHandler
    {
        private static int cardNumber = -1;
        private static int additionalQuestionsNumber = 0;
        private static List<string> symptomsNames = new List<string>() { "���� �� �������� ��� � ������� ������", "��������� ���� �� �������� ��� � ������� ������", "���� �� �������� ��� � ������� ������ ��� ���������� ����������", "������� ���� �� �������� ��� � ������� ������", "������� ���� �� �������� ��� � ������� ������", "������� ���� �� �������� ��� � ������� ������", "����� ���� �� �������� ��� � ������� ������", "���� � ������", "��������� � ������ ������", "���� � ��������", "���� � ���", "���� ��� �������", "���� � ������", "���� � ������", "���� � ������ ���", "���� � ���������� ������", "���� � ������ ���", "���� � ��������", "���� � �������", "���� � ������ �����������", "������ ���� � ������ �����������", "���� � ������ ����������� ��� ������", "���� � ������� ���", "���� � ������� �����������", "���� � ������� ���", "��������� ������������", "����������� ������������", "������� � ������ ������", "�������� ������", "�������� ��������� � �����", "������ � �����", "���������� ���� �� ������� ������ � ������ ����� ����", "������", "������ ���������� ��������", "������ ��� ���������� ����������", "��������", "������ � ��������� ���� ��� ����", "�������� �������", "���������� �������", "�������", "�����", "������� �������", "��������", "���������", "�������� ����", "�������� �������", "�������� ������� �����������", "�������� ������� ����������� � ����� �������", "�������� ������� ���", "�������� ������ �����������", "�������� ������ ����������� � ����� �������", "�������� ������� ���", "��� �����", "��� ����", "���� �� ���� ������", "��� ���", "���� ������ �����������", "���� ������� �����������", "���������� �������������", "�������� ���", "�������� ����", "�������� ���� � �������� �����", "�������� ���� � ���������� �����", "������������ �������� ����", "������������ �������� ����", "��������������", "�������� ��� ���������� ���������", "��� � ����", "��� � ������", "��e������ ������", "������� ����� �������", "���������� � ������", "���������� �������", "��������� ��������", "������ ������������ �� ������� � ������������", "���������������", "������������ ��������", "������", "��������� ���", "��������� ����", "�������� � ���������", "������������� �����������", "�������� �������", "��������������", "����������� ������������ �������", "����������� ������������ ������� ������ �����������", "����������� ������������ ������� ������� �����������", "����������� ������������ ������� ������ ����������� � ����� ������� ����", "����������� ������������ ������� ������� ����������� � ����� ������� ����", "������� ����", "�����", "�������� �������", "�������� ������ �����������", "�������� ������� ���", "�������� ������� �����������", "�������� ������� ���", "��������� �����������", "������ ����������� ����", "������ ����������� �������", "������ ����������� ���", "������ ����������� ������� ���", "������ ����������� ���", "������ ����������� ������� ���", "��������� ��������", "��������� ��������", "������ �������� ����������", "����������� ����", "����������� ����", "����������� �������", "����������� ������ �����������", "����������� ������� ���", "����������� ������� �����������", "����������� ������� ���", "����������� ����", "����������� ���", "����������� ����", "����������� ����", "����������� ���� ������", "��������� ����", "��������� ����", "��������� �������", "��������� ������ �����������", "��������� ������� ���", "��������� ������� �����������", "��������� ������� ���", "��������� ����", "��������� ���", "��������� ����", "���������� ����", "������ ������ �����������", "������ �������", "������ ������� ���", "������ ������� �����������", "������ ������� ���", "������ ����", "������ ����", "������ ���", "������ ����", "�������� ����", "�������� ����", "������� ����", "������� ���� ����", "������� ���� ���", "������� ���� ���", "��������� ����", "������� ������������", "���������� ������������", "����� ����", "������ ����", "������� ����", "������ ��������", "������", "������ ����������� � ��������� ���", "����� ������", "�������", "�����", "�������������", "��������� ��� �� ���", "���������� ������", "���� � ������ ����������", "������� ������", "��������� ���������� ���", "��������� ��� �� �������", "��������� ��� �� ������ �����������", "��������� ��� �� ������� ���", "��������� ��� �� ������� �����������", "��������� ��� �� ������� ���", "�������� � ������ �����������", "�������� � ������ ����������� ��������� �� �����", "�������� � ������� �����������", "������� ������������ ���", "������� ������������ ���", "������� � �����", "������ � �����", "��� ����", "��� ���� ����", "��� ���� ���", "��� ���� ���", "���������� ��������", "���������� �������� �� ����", "���������� �������� �� �����", "���������� �������� �� �����", "������������� ������", "���������� ����", "���������� ������ �����������", "���������� �������", "���������� ������� ���", "���������� ������� �����������", "���������� ������� ���", "���������� ����", "���������� ����", "���������� ���", "���������� ����", "�������� �����", "��������� �����", "��������� ����� �� �����", "��������� ����� �� �����", "�����", "�����", "��������", "������", "������ ������ �����������", "������ ������� �����������", "������ �������", "������ ������� ���", "������ ������� ���", "����", "���� �� �����", "���� �� �����", "�������� � �������", "������� �� ������", "����������", "����������� ��������������", "��������������������� ����", "��������������������� �������", "��������������������� ���� ������ �����������", "��������������������� ������� ���", "��������������������� ���� ������� �����������", "��������������������� ������� ���", "����� � ����", "����� � ����", "������", "������ ����", "����������", "��������� ����� ��� ���", "�������������" };
        private static List<int> symptomsList = new List<int>();
        private static Controllers.CurrentSymptoms currentSymptoms = new Controllers.CurrentSymptoms(new List<int>());

        private static Controllers.AnginaPectoris ap = new Controllers.AnginaPectoris();
        private static Controllers.Arrhythmia ar = new Controllers.Arrhythmia();
        private static Controllers.CoronaryHeartDisease chd = new Controllers.CoronaryHeartDisease();
        private static Controllers.CrerebralCirculationViolation ccv = new Controllers.CrerebralCirculationViolation();
        private static Controllers.HeartCystonia heartCys = new Controllers.HeartCystonia();
        private static Controllers.Hypertension hyper = new Controllers.Hypertension();
        private static Controllers.HypertensionCystonia hyperCys = new Controllers.HypertensionCystonia();
        private static Controllers.HypotensionCystonia hypoCys = new Controllers.HypotensionCystonia();
        private static Controllers.LeftChronicHeartFailure lchf = new Controllers.LeftChronicHeartFailure();
        private static Controllers.Myocarditis myo = new Controllers.Myocarditis();
        private static Controllers.ParasympatheticCystonia pc = new Controllers.ParasympatheticCystonia();
        private static Controllers.PulmonaryEmbolism pe = new Controllers.PulmonaryEmbolism();
        private static Controllers.RaynaudSyndrome rs = new Controllers.RaynaudSyndrome();
        private static Controllers.RheumaticFever rf = new Controllers.RheumaticFever();
        private static Controllers.RightChronicHeartFailure rchf = new Controllers.RightChronicHeartFailure();
        private static Controllers.VaricoseVeins vv = new Controllers.VaricoseVeins();
        private static Controllers.AcuteKidneyInjury aki = new Controllers.AcuteKidneyInjury();
        private static Controllers.Angiopathy ang = new Controllers.Angiopathy();
        private static Controllers.Cardiomyopathy cm = new Controllers.Cardiomyopathy();
        private static Controllers.Cardiosclerosis cs = new Controllers.Cardiosclerosis();
        private static Controllers.CerebralAtherosclerosis ca = new Controllers.CerebralAtherosclerosis();
        private static Controllers.Collapsing col = new Controllers.Collapsing();
        private static Controllers.CongenitalHeartDefect conhd = new Controllers.CongenitalHeartDefect();
        private static Controllers.Endarteritis end = new Controllers.Endarteritis();
        private static Controllers.HortonDisease hor = new Controllers.HortonDisease();
        private static Controllers.InfectiveEndocarditis ie = new Controllers.InfectiveEndocarditis();
        private static Controllers.LerichSyndrome ls = new Controllers.LerichSyndrome();
        private static Controllers.LowerExtremityAtherosclerosis lea = new Controllers.LowerExtremityAtherosclerosis();
        private static Controllers.Miocardiodystrophy md = new Controllers.Miocardiodystrophy();
        private static Controllers.MitralValveProlaps mvp = new Controllers.MitralValveProlaps();
        private static Controllers.MyocardialInfarction mi = new Controllers.MyocardialInfarction();
        private static Controllers.ObliteratingThromboangiitis ot = new Controllers.ObliteratingThromboangiitis();
        private static Controllers.Pericarditis per = new Controllers.Pericarditis();
        private static Controllers.PolyarteritisNodosa pn = new Controllers.PolyarteritisNodosa();
        private static Controllers.PulmonaryHeart ph = new Controllers.PulmonaryHeart();
        private static Controllers.RenalArteryAtherosclerosis raa = new Controllers.RenalArteryAtherosclerosis();
        private static Controllers.TakayasuArteritis ta = new Controllers.TakayasuArteritis();
        private static Controllers.UpperExtrimityAtherosclerosis uea = new Controllers.UpperExtrimityAtherosclerosis();
        private static Controllers.ValvularHeartDisease vhd = new Controllers.ValvularHeartDisease();
        private static Controllers.SympatheticCystonia sc = new Controllers.SympatheticCystonia();
        private static int illness_id_1 = -1;
        private static int illness_id_2 = -1;
        private static int illness_id_3 = -1;
        private static int count_of_same = 1;
        private static int count_of_high = 0;
        private static int actual_number = -1;
        private static int message_number = 0;
        private static int answer_number = 0;
        private static string path = "answers.txt";

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Text!= null && turnContext.Activity.Text.ToLower() == "!�������")
            {
                cardNumber = -1;
                additionalQuestionsNumber = 0;
                illness_id_1 = -1;
                illness_id_2 = -1;
                illness_id_3 = -1;
                count_of_same = 1;
                count_of_high = 0;
                actual_number = -1;
                symptomsList.Clear();
                currentSymptoms = new Controllers.CurrentSymptoms(new List<int>());
                await File.AppendAllTextAsync(path, $"{turnContext.Activity.Recipient.Id} -1 {turnContext.Activity.Text}\n");
                await MakeAgeQuestion(turnContext, cancellationToken);
                answer_number = 0;
                message_number = 1;
            }
            else
            {
                if (turnContext.Activity.Text != null && turnContext.Activity.Text.ToLower() == "!����������")
                {
                    cardNumber = -1;
                    additionalQuestionsNumber = 0;
                    illness_id_1 = -1;
                    illness_id_2 = -1;
                    illness_id_3 = -1;
                    count_of_same = 1;
                    count_of_high = 0;
                    actual_number = -1;
                    symptomsList.Clear();
                    currentSymptoms = new Controllers.CurrentSymptoms(new List<int>());
                    await File.AppendAllTextAsync(path, $"{turnContext.Activity.Recipient.Id} -2 {turnContext.Activity.Text}\n");
                    await MakeSymptomsList(turnContext, cancellationToken);
                    answer_number = 17;
                    message_number = 18;
                }
                else
                {
                    if (turnContext.Activity.Text != null)
                    {
                        await File.AppendAllTextAsync(path, $"{turnContext.Activity.Recipient.Id} {answer_number} {turnContext.Activity.Text}\n");
                    }
                    switch (message_number)
                    {
                        case 0:
                            turnContext.Activity.InputHint = "ignoringInput";
                            await MakeAgeQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 1:
                            turnContext.Activity.InputHint = "ignoringInput";
                            await MakeSexQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 2:
                            await MakeWeightQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 3:
                            await MakeHeightQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 4:
                            await MakeCVDQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 5:
                            if (turnContext.Activity.Text == "��")
                            {
                                await MakeCVDList(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 1;
                            }
                            else
                            {
                                await MakeCDQuestion(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 2;
                            }
                            break;
                        case 6:
                            await MakeCDQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 7:
                            if (turnContext.Activity.Text == "��")
                            {
                                await MakeCDList(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 1;
                            }
                            else
                            {
                                await MakeIDQuestion(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 2;
                            }
                            break;
                        case 8:
                            await MakeIDQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 9:
                            if (turnContext.Activity.Text == "��")
                            {
                                await MakeIDList(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 1;
                            }
                            else
                            {
                                await MakeRDQuestion(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number += 2;
                            }
                            break;
                        case 10:
                            await MakeRDQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 11:
                            await MakeAlcoholQuestion(turnContext, cancellationToken);
                            message_number += 1;
                            break;
                        case 12:
                            await MakeSmokeQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 13:
                            await MakeFoodQualityQuestion(turnContext, cancellationToken);
                            message_number += 1;
                            break;
                        case 14:
                            await MakeActivityQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;

                        case 15:
                            await MakeAllergyQuestion(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 16:
                            await MakeInjuryQuestion(turnContext, cancellationToken);
                            message_number += 1;
                            break;
                        case 17:
                            turnContext.Activity.InputHint = "ignoringInput";
                            await MakeSymptomsList(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 18:
                            turnContext.Activity.InputHint = "ignoringInput";
                            await MakeCorrections1(turnContext, cancellationToken);
                            message_number += 1;
                            break;
                        case 19:
                            turnContext.Activity.InputHint = "ignoringInput";
                            if (turnContext.Activity.Text == "�������� ��������")
                            {
                                await AddSymptoms(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number = 18;
                                break;
                            }
                            else
                            {
                                if (turnContext.Activity.Text == "������ ��������")
                                {
                                    await DeleteSymptoms(turnContext, cancellationToken);
                                    answer_number = message_number;
                                    message_number = 20;
                                    break;
                                }
                                else
                                {
                                    currentSymptoms = new Controllers.CurrentSymptoms(symptomsList);
                                    currentSymptoms.RecognizeSymptoms();
                                    await turnContext.SendActivityAsync("������ � ����� ��������� ����������� �������� �� ����� ���������. ���� �� ������, �������� �����-������ ���������.");
                                    answer_number = message_number;
                                    message_number = 22;
                                    break;
                                }
                            }
                        case 20:
                            turnContext.Activity.InputHint = "ignoringInput";
                            await MakeCorrections2(turnContext, cancellationToken);
                            answer_number = message_number;
                            message_number += 1;
                            break;
                        case 21:
                            turnContext.Activity.InputHint = "ignoringInput";
                            if (turnContext.Activity.Text == "�������� ��������")
                            {
                                await AddSymptoms(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number = 18;
                                break;
                            }
                            else
                            {
                                if (turnContext.Activity.Text == "������ ��������")
                                {
                                    await DeleteSymptoms(turnContext, cancellationToken);
                                    answer_number = message_number;
                                    message_number = 20;
                                    break;
                                }
                                else
                                {
                                    currentSymptoms = new Controllers.CurrentSymptoms(symptomsList);
                                    currentSymptoms.RecognizeSymptoms();
                                    await turnContext.SendActivityAsync("������ � ����� ��������� ����������� �������� �� ����� ���������. ���� �� ������, �������� �����-������ ���������.");
                                    answer_number = message_number;
                                    message_number = 22;
                                    break;
                                }
                            }
                        case 22:
                            turnContext.Activity.InputHint = "ignoringInput";
                            string[] networkResults = SendToNetwork(currentSymptoms.symptomsString).Split(' ');
                            int number = -1;
                            if (illness_id_1.ToString() == networkResults[1].ToString() && illness_id_2.ToString() == networkResults[3] && illness_id_3.ToString() == networkResults[5]) ++count_of_same;
                            else
                            {
                                count_of_same = 1;
                                illness_id_1 = int.Parse(networkResults[1]);
                                illness_id_2 = int.Parse(networkResults[3]);
                                illness_id_3 = int.Parse(networkResults[5]);
                            }
                            Console.WriteLine(networkResults[0]);
                            if (double.Parse(networkResults[0], System.Globalization.CultureInfo.InvariantCulture) > 0.9) ++count_of_high;
                            else count_of_high = 0;
                            if (actual_number != -1 && turnContext.Activity.Text == "���")
                            {
                                number = actual_number;

                                cardNumber = number;
                                await SendCard(cardNumber, turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number = 23;
                            }
                            else
                            {
                                if (additionalQuestionsNumber < 5 || (additionalQuestionsNumber < 20 && count_of_high < 3 && count_of_same < 3))
                                {
                                    number = FindNumber(networkResults);
                                    actual_number = number;
                                }
                                if (number != -1)
                                {
                                    cardNumber = number;
                                    await SendCard(cardNumber, turnContext, cancellationToken);
                                    ++additionalQuestionsNumber;
                                    answer_number = message_number;
                                    message_number = 23;
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync("��� ��� ��������� ������. ��������� �����-������ ���������, ����� ������� � �����������.");
                                    answer_number = message_number;
                                    message_number = 24;
                                }
                            }
                            break;
                        case 23:
                            bool successFlag = false;
                            if (turnContext.Activity.Text == "��")
                            {
                                currentSymptoms.RecognizeTrueSymptom(actual_number);
                                successFlag = true;
                            }
                            if (turnContext.Activity.Text == "���")
                            {
                                currentSymptoms.RecognizeNegativeSymptom(actual_number);
                                successFlag = true;
                            }
                            if (turnContext.Activity.Text == "�� ����")
                            {
                                currentSymptoms.RecognizeProbableSymptom(actual_number);
                                successFlag = true;
                            }
                            if (!successFlag)
                            {
                                await turnContext.SendActivityAsync("�����, ����������, ���� �� ��������� ������, ��������� �� �������� � ��������");
                                break;
                            }
                            await turnContext.SendActivityAsync("�� �������?");
                            answer_number = message_number;
                            message_number = 22;
                            break;
                        case 24:
                            turnContext.Activity.InputHint = "ignoringInput";
                            networkResults = SendToNetwork(currentSymptoms.symptomsString).Split(' ');
                            await PrintResults(networkResults, turnContext, cancellationToken);

                            cardNumber = -1;
                            additionalQuestionsNumber = 0;
                            illness_id_1 = -1;
                            illness_id_2 = -1;
                            illness_id_3 = -1;
                            count_of_same = 1;
                            count_of_high = 0;
                            actual_number = -1;
                            answer_number = message_number;
                            message_number = 17;
                            symptomsList.Clear();
                            currentSymptoms = new Controllers.CurrentSymptoms(new List<int>());
                            break;
                    }
                }
            }

        }

        protected async Task PrintResults(string[] results, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            double max1 = double.Parse(results[0], System.Globalization.CultureInfo.InvariantCulture);
            double max2 = double.Parse(results[2], System.Globalization.CultureInfo.InvariantCulture);
            double max3 = double.Parse(results[4], System.Globalization.CultureInfo.InvariantCulture);
            int i1 = int.Parse(results[1]);
            int i2 = int.Parse(results[3]);
            int i3 = int.Parse(results[5]);
            await SendFinalCard(i1 - 1, i2 - 1, i3 - 1, max1, max2, max3, turnContext, cancellationToken);
        }

        protected int FindNumber(string[] networkResult)
        {
            List<double> user_str = currentSymptoms.symptomsString;
            for (int i = 0; i < user_str.Count; i++)
            {
                Console.Write(user_str[i] + " ");
            }
            Console.WriteLine();
            List<double> symp_str_1 = new List<double>();
            List<double> symp_str_2 = new List<double>();
            List<double> symp_str_3 = new List<double>();
            double probability_1 = 0, probability_2 = 0, probability_3 = 0;
            if (int.Parse(networkResult[1]) == 1)
            {
                string[] symptoms_string = ie.symptoms_numbers.Split(' ');
                string[] reliability_string = ie.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ie.prevalence;

            }

            if (int.Parse(networkResult[3]) == 1)
            {
                string[] symptoms_string = ie.symptoms_numbers.Split(' ');
                string[] reliability_string = ie.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ie.prevalence;

            }

            if (int.Parse(networkResult[5]) == 1)
            {
                string[] symptoms_string = ie.symptoms_numbers.Split(' ');
                string[] reliability_string = ie.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ie.prevalence;

            }

            if (int.Parse(networkResult[1]) == 2)
            {
                string[] symptoms_string = pn.symptoms_numbers.Split(' ');
                string[] reliability_string = pn.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = pn.prevalence;

            }

            if (int.Parse(networkResult[3]) == 2)
            {
                string[] symptoms_string = pn.symptoms_numbers.Split(' ');
                string[] reliability_string = pn.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = pn.prevalence;

            }

            if (int.Parse(networkResult[5]) == 2)
            {
                string[] symptoms_string = pn.symptoms_numbers.Split(' ');
                string[] reliability_string = ie.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = pn.prevalence;

            }

            if (int.Parse(networkResult[1]) == 3)
            {
                string[] symptoms_string = ta.symptoms_numbers.Split(' ');
                string[] reliability_string = ta.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ta.prevalence;

            }

            if (int.Parse(networkResult[3]) == 3)
            {
                string[] symptoms_string = ta.symptoms_numbers.Split(' ');
                string[] reliability_string = ta.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ta.prevalence;

            }

            if (int.Parse(networkResult[5]) == 3)
            {
                string[] symptoms_string = ta.symptoms_numbers.Split(' ');
                string[] reliability_string = ta.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ta.prevalence;

            }

            if (int.Parse(networkResult[1]) == 4)
            {
                string[] symptoms_string = hor.symptoms_numbers.Split(' ');
                string[] reliability_string = hor.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = hor.prevalence;

            }

            if (int.Parse(networkResult[3]) == 4)
            {
                string[] symptoms_string = hor.symptoms_numbers.Split(' ');
                string[] reliability_string = hor.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = hor.prevalence;

            }

            if (int.Parse(networkResult[5]) == 4)
            {
                string[] symptoms_string = hor.symptoms_numbers.Split(' ');
                string[] reliability_string = hor.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = hor.prevalence;

            }

            if (int.Parse(networkResult[1]) == 5)
            {
                string[] symptoms_string = raa.symptoms_numbers.Split(' ');
                string[] reliability_string = raa.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = raa.prevalence;

            }

            if (int.Parse(networkResult[3]) == 5)
            {
                string[] symptoms_string = raa.symptoms_numbers.Split(' ');
                string[] reliability_string = raa.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = raa.prevalence;

            }

            if (int.Parse(networkResult[5]) == 5)
            {
                string[] symptoms_string = raa.symptoms_numbers.Split(' ');
                string[] reliability_string = raa.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = raa.prevalence;

            }

            if (int.Parse(networkResult[1]) == 6)
            {
                string[] symptoms_string = aki.symptoms_numbers.Split(' ');
                string[] reliability_string = aki.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = aki.prevalence;

            }

            if (int.Parse(networkResult[3]) == 6)
            {
                string[] symptoms_string = aki.symptoms_numbers.Split(' ');
                string[] reliability_string = aki.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = aki.prevalence;

            }

            if (int.Parse(networkResult[5]) == 6)
            {
                string[] symptoms_string = aki.symptoms_numbers.Split(' ');
                string[] reliability_string = aki.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = aki.prevalence;

            }

            if (int.Parse(networkResult[1]) == 7)
            {
                string[] symptoms_string = cs.symptoms_numbers.Split(' ');
                string[] reliability_string = cs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = cs.prevalence;

            }

            if (int.Parse(networkResult[3]) == 7)
            {
                string[] symptoms_string = cs.symptoms_numbers.Split(' ');
                string[] reliability_string = cs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = cs.prevalence;

            }

            if (int.Parse(networkResult[5]) == 7)
            {
                string[] symptoms_string = cs.symptoms_numbers.Split(' ');
                string[] reliability_string = cs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = cs.prevalence;

            }

            if (int.Parse(networkResult[1]) == 8)
            {
                string[] symptoms_string = ar.symptoms_numbers.Split(' ');
                string[] reliability_string = ar.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ar.prevalence;

            }

            if (int.Parse(networkResult[3]) == 8)
            {
                string[] symptoms_string = ar.symptoms_numbers.Split(' ');
                string[] reliability_string = ar.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ar.prevalence;

            }

            if (int.Parse(networkResult[5]) == 8)
            {
                string[] symptoms_string = ar.symptoms_numbers.Split(' ');
                string[] reliability_string = ar.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ar.prevalence;

            }

            if (int.Parse(networkResult[1]) == 9)
            {
                string[] symptoms_string = hyper.symptoms_numbers.Split(' ');
                string[] reliability_string = hyper.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = hyper.prevalence;

            }

            if (int.Parse(networkResult[3]) == 9)
            {
                string[] symptoms_string = hyper.symptoms_numbers.Split(' ');
                string[] reliability_string = hyper.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = hyper.prevalence;

            }

            if (int.Parse(networkResult[5]) == 9)
            {
                string[] symptoms_string = hyper.symptoms_numbers.Split(' ');
                string[] reliability_string = hyper.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = hyper.prevalence;

            }

            if (int.Parse(networkResult[1]) == 10)
            {
                string[] symptoms_string = pc.symptoms_numbers.Split(' ');
                string[] reliability_string = pc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = pc.prevalence;

            }

            if (int.Parse(networkResult[3]) == 10)
            {
                string[] symptoms_string = pc.symptoms_numbers.Split(' ');
                string[] reliability_string = pc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = pc.prevalence;

            }

            if (int.Parse(networkResult[5]) == 10)
            {
                string[] symptoms_string = pc.symptoms_numbers.Split(' ');
                string[] reliability_string = pc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = pc.prevalence;

            }

            if (int.Parse(networkResult[1]) == 11)
            {
                string[] symptoms_string = hypoCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hypoCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = hypoCys.prevalence;

            }

            if (int.Parse(networkResult[3]) == 11)
            {
                string[] symptoms_string = hypoCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hypoCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = hypoCys.prevalence;

            }

            if (int.Parse(networkResult[5]) == 11)
            {
                string[] symptoms_string = hypoCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hypoCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = hypoCys.prevalence;

            }

            if (int.Parse(networkResult[1]) == 12)
            {
                string[] symptoms_string = hyperCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hyperCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = hyperCys.prevalence;

            }

            if (int.Parse(networkResult[3]) == 12)
            {
                string[] symptoms_string = hyperCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hyperCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = hyperCys.prevalence;

            }

            if (int.Parse(networkResult[5]) == 12)
            {
                string[] symptoms_string = hyperCys.symptoms_numbers.Split(' ');
                string[] reliability_string = hyperCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = hyperCys.prevalence;

            }

            if (int.Parse(networkResult[1]) == 13)
            {
                string[] symptoms_string = sc.symptoms_numbers.Split(' ');
                string[] reliability_string = sc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = sc.prevalence;

            }

            if (int.Parse(networkResult[3]) == 13)
            {
                string[] symptoms_string = sc.symptoms_numbers.Split(' ');
                string[] reliability_string = sc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = sc.prevalence;

            }

            if (int.Parse(networkResult[5]) == 13)
            {
                string[] symptoms_string = sc.symptoms_numbers.Split(' ');
                string[] reliability_string = sc.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = sc.prevalence;

            }

            if (int.Parse(networkResult[1]) == 14)
            {
                string[] symptoms_string = heartCys.symptoms_numbers.Split(' ');
                string[] reliability_string = heartCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = heartCys.prevalence;

            }

            if (int.Parse(networkResult[3]) == 14)
            {
                string[] symptoms_string = heartCys.symptoms_numbers.Split(' ');
                string[] reliability_string = heartCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = heartCys.prevalence;

            }

            if (int.Parse(networkResult[5]) == 14)
            {
                string[] symptoms_string = heartCys.symptoms_numbers.Split(' ');
                string[] reliability_string = heartCys.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = heartCys.prevalence;

            }

            if (int.Parse(networkResult[1]) == 15)
            {
                string[] symptoms_string = md.symptoms_numbers.Split(' ');
                string[] reliability_string = md.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = md.prevalence;

            }

            if (int.Parse(networkResult[3]) == 15)
            {
                string[] symptoms_string = md.symptoms_numbers.Split(' ');
                string[] reliability_string = md.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = md.prevalence;

            }

            if (int.Parse(networkResult[5]) == 15)
            {
                string[] symptoms_string = md.symptoms_numbers.Split(' ');
                string[] reliability_string = md.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = md.prevalence;

            }

            if (int.Parse(networkResult[1]) == 16)
            {
                string[] symptoms_string = rf.symptoms_numbers.Split(' ');
                string[] reliability_string = rf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = rf.prevalence;

            }

            if (int.Parse(networkResult[3]) == 16)
            {
                string[] symptoms_string = rf.symptoms_numbers.Split(' ');
                string[] reliability_string = rf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = rf.prevalence;

            }

            if (int.Parse(networkResult[5]) == 16)
            {
                string[] symptoms_string = rf.symptoms_numbers.Split(' ');
                string[] reliability_string = rf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = rf.prevalence;

            }

            if (int.Parse(networkResult[1]) == 17)
            {
                string[] symptoms_string = myo.symptoms_numbers.Split(' ');
                string[] reliability_string = myo.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = myo.prevalence;

            }

            if (int.Parse(networkResult[3]) == 17)
            {
                string[] symptoms_string = myo.symptoms_numbers.Split(' ');
                string[] reliability_string = myo.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = myo.prevalence;

            }

            if (int.Parse(networkResult[5]) == 17)
            {
                string[] symptoms_string = myo.symptoms_numbers.Split(' ');
                string[] reliability_string = myo.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = myo.prevalence;

            }

            if (int.Parse(networkResult[1]) == 18)
            {
                string[] symptoms_string = pe.symptoms_numbers.Split(' ');
                string[] reliability_string = pe.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = pe.prevalence;

            }

            if (int.Parse(networkResult[3]) == 18)
            {
                string[] symptoms_string = pe.symptoms_numbers.Split(' ');
                string[] reliability_string = pe.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = pe.prevalence;

            }

            if (int.Parse(networkResult[5]) == 18)
            {
                string[] symptoms_string = pe.symptoms_numbers.Split(' ');
                string[] reliability_string = pe.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = pe.prevalence;

            }

            if (int.Parse(networkResult[1]) == 19)
            {
                string[] symptoms_string = ph.symptoms_numbers.Split(' ');
                string[] reliability_string = ph.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ph.prevalence;

            }

            if (int.Parse(networkResult[3]) == 19)
            {
                string[] symptoms_string = ph.symptoms_numbers.Split(' ');
                string[] reliability_string = ph.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ph.prevalence;

            }

            if (int.Parse(networkResult[5]) == 19)
            {
                string[] symptoms_string = ph.symptoms_numbers.Split(' ');
                string[] reliability_string = ph.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ph.prevalence;

            }

            if (int.Parse(networkResult[1]) == 20)
            {
                string[] symptoms_string = ap.symptoms_numbers.Split(' ');
                string[] reliability_string = ap.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ap.prevalence;

            }

            if (int.Parse(networkResult[3]) == 20)
            {
                string[] symptoms_string = ap.symptoms_numbers.Split(' ');
                string[] reliability_string = ap.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ap.prevalence;

            }

            if (int.Parse(networkResult[5]) == 20)
            {
                string[] symptoms_string = ap.symptoms_numbers.Split(' ');
                string[] reliability_string = ap.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ap.prevalence;

            }

            if (int.Parse(networkResult[1]) == 21)
            {
                string[] symptoms_string = chd.symptoms_numbers.Split(' ');
                string[] reliability_string = chd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = chd.prevalence;

            }

            if (int.Parse(networkResult[3]) == 21)
            {
                string[] symptoms_string = chd.symptoms_numbers.Split(' ');
                string[] reliability_string = chd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = chd.prevalence;

            }

            if (int.Parse(networkResult[5]) == 21)
            {
                string[] symptoms_string = chd.symptoms_numbers.Split(' ');
                string[] reliability_string = chd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = chd.prevalence;

            }

            if (int.Parse(networkResult[1]) == 22)
            {
                string[] symptoms_string = mi.symptoms_numbers.Split(' ');
                string[] reliability_string = mi.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = mi.prevalence;

            }

            if (int.Parse(networkResult[3]) == 22)
            {
                string[] symptoms_string = mi.symptoms_numbers.Split(' ');
                string[] reliability_string = mi.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = mi.prevalence;

            }

            if (int.Parse(networkResult[5]) == 22)
            {
                string[] symptoms_string = mi.symptoms_numbers.Split(' ');
                string[] reliability_string = mi.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = mi.prevalence;

            }

            if (int.Parse(networkResult[1]) == 23)
            {
                string[] symptoms_string = mvp.symptoms_numbers.Split(' ');
                string[] reliability_string = mvp.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = mvp.prevalence;

            }

            if (int.Parse(networkResult[3]) == 23)
            {
                string[] symptoms_string = mvp.symptoms_numbers.Split(' ');
                string[] reliability_string = mvp.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = mvp.prevalence;

            }

            if (int.Parse(networkResult[5]) == 23)
            {
                string[] symptoms_string = mvp.symptoms_numbers.Split(' ');
                string[] reliability_string = mvp.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = mvp.prevalence;

            }

            if (int.Parse(networkResult[1]) == 24)
            {
                string[] symptoms_string = cm.symptoms_numbers.Split(' ');
                string[] reliability_string = cm.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = cm.prevalence;

            }

            if (int.Parse(networkResult[3]) == 24)
            {
                string[] symptoms_string = cm.symptoms_numbers.Split(' ');
                string[] reliability_string = cm.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = cm.prevalence;

            }

            if (int.Parse(networkResult[5]) == 24)
            {
                string[] symptoms_string = cm.symptoms_numbers.Split(' ');
                string[] reliability_string = cm.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = cm.prevalence;

            }

            if (int.Parse(networkResult[1]) == 25)
            {
                string[] symptoms_string = per.symptoms_numbers.Split(' ');
                string[] reliability_string = per.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = per.prevalence;

            }

            if (int.Parse(networkResult[3]) == 25)
            {
                string[] symptoms_string = per.symptoms_numbers.Split(' ');
                string[] reliability_string = per.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = per.prevalence;

            }

            if (int.Parse(networkResult[5]) == 25)
            {
                string[] symptoms_string = per.symptoms_numbers.Split(' ');
                string[] reliability_string = per.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = per.prevalence;

            }

            if (int.Parse(networkResult[1]) == 27)
            {
                string[] symptoms_string = conhd.symptoms_numbers.Split(' ');
                string[] reliability_string = conhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = conhd.prevalence;

            }

            if (int.Parse(networkResult[3]) == 27)
            {
                string[] symptoms_string = conhd.symptoms_numbers.Split(' ');
                string[] reliability_string = conhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = conhd.prevalence;

            }

            if (int.Parse(networkResult[5]) == 27)
            {
                string[] symptoms_string = conhd.symptoms_numbers.Split(' ');
                string[] reliability_string = conhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = conhd.prevalence;

            }

            if (int.Parse(networkResult[1]) == 26)
            {
                string[] symptoms_string = vhd.symptoms_numbers.Split(' ');
                string[] reliability_string = vhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = vhd.prevalence;

            }

            if (int.Parse(networkResult[3]) == 26)
            {
                string[] symptoms_string = vhd.symptoms_numbers.Split(' ');
                string[] reliability_string = vhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = vhd.prevalence;

            }

            if (int.Parse(networkResult[5]) == 26)
            {
                string[] symptoms_string = vhd.symptoms_numbers.Split(' ');
                string[] reliability_string = vhd.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = vhd.prevalence;

            }

            if (int.Parse(networkResult[1]) == 28)
            {
                string[] symptoms_string = rchf.symptoms_numbers.Split(' ');
                string[] reliability_string = rchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = rchf.prevalence;

            }

            if (int.Parse(networkResult[3]) == 28)
            {
                string[] symptoms_string = rchf.symptoms_numbers.Split(' ');
                string[] reliability_string = rchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = rchf.prevalence;

            }

            if (int.Parse(networkResult[5]) == 28)
            {
                string[] symptoms_string = rchf.symptoms_numbers.Split(' ');
                string[] reliability_string = rchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = rchf.prevalence;

            }

            if (int.Parse(networkResult[1]) == 29)
            {
                string[] symptoms_string = lchf.symptoms_numbers.Split(' ');
                string[] reliability_string = lchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = lchf.prevalence;

            }

            if (int.Parse(networkResult[3]) == 29)
            {
                string[] symptoms_string = lchf.symptoms_numbers.Split(' ');
                string[] reliability_string = lchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = lchf.prevalence;

            }

            if (int.Parse(networkResult[5]) == 29)
            {
                string[] symptoms_string = lchf.symptoms_numbers.Split(' ');
                string[] reliability_string = lchf.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = lchf.prevalence;

            }

            if (int.Parse(networkResult[1]) == 30)
            {
                string[] symptoms_string = ca.symptoms_numbers.Split(' ');
                string[] reliability_string = ca.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ca.prevalence;

            }

            if (int.Parse(networkResult[3]) == 30)
            {
                string[] symptoms_string = ca.symptoms_numbers.Split(' ');
                string[] reliability_string = ca.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ca.prevalence;

            }

            if (int.Parse(networkResult[5]) == 30)
            {
                string[] symptoms_string = ca.symptoms_numbers.Split(' ');
                string[] reliability_string = ca.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ca.prevalence;

            }

            if (int.Parse(networkResult[1]) == 31)
            {
                string[] symptoms_string = ccv.symptoms_numbers.Split(' ');
                string[] reliability_string = ccv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ccv.prevalence;

            }

            if (int.Parse(networkResult[3]) == 31)
            {
                string[] symptoms_string = ccv.symptoms_numbers.Split(' ');
                string[] reliability_string = ccv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ccv.prevalence;

            }

            if (int.Parse(networkResult[5]) == 31)
            {
                string[] symptoms_string = ccv.symptoms_numbers.Split(' ');
                string[] reliability_string = ccv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ccv.prevalence;

            }

            if (int.Parse(networkResult[1]) == 32)
            {
                string[] symptoms_string = col.symptoms_numbers.Split(' ');
                string[] reliability_string = col.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = col.prevalence;

            }

            if (int.Parse(networkResult[3]) == 32)
            {
                string[] symptoms_string = col.symptoms_numbers.Split(' ');
                string[] reliability_string = col.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = col.prevalence;

            }

            if (int.Parse(networkResult[5]) == 32)
            {
                string[] symptoms_string = col.symptoms_numbers.Split(' ');
                string[] reliability_string = col.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = col.prevalence;

            }

            if (int.Parse(networkResult[1]) == 33)
            {
                string[] symptoms_string = lea.symptoms_numbers.Split(' ');
                string[] reliability_string = lea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = lea.prevalence;

            }

            if (int.Parse(networkResult[3]) == 33)
            {
                string[] symptoms_string = lea.symptoms_numbers.Split(' ');
                string[] reliability_string = lea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = lea.prevalence;

            }

            if (int.Parse(networkResult[5]) == 33)
            {
                string[] symptoms_string = lea.symptoms_numbers.Split(' ');
                string[] reliability_string = lea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = lea.prevalence;

            }

            if (int.Parse(networkResult[1]) == 34)
            {
                string[] symptoms_string = vv.symptoms_numbers.Split(' ');
                string[] reliability_string = vv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = vv.prevalence;

            }

            if (int.Parse(networkResult[3]) == 34)
            {
                string[] symptoms_string = vv.symptoms_numbers.Split(' ');
                string[] reliability_string = vv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = vv.prevalence;

            }

            if (int.Parse(networkResult[5]) == 34)
            {
                string[] symptoms_string = vv.symptoms_numbers.Split(' ');
                string[] reliability_string = vv.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = vv.prevalence;

            }

            if (int.Parse(networkResult[1]) == 35)
            {
                string[] symptoms_string = end.symptoms_numbers.Split(' ');
                string[] reliability_string = end.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = end.prevalence;

            }

            if (int.Parse(networkResult[3]) == 35)
            {
                string[] symptoms_string = end.symptoms_numbers.Split(' ');
                string[] reliability_string = end.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = end.prevalence;

            }

            if (int.Parse(networkResult[5]) == 35)
            {
                string[] symptoms_string = end.symptoms_numbers.Split(' ');
                string[] reliability_string = end.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = end.prevalence;

            }

            if (int.Parse(networkResult[1]) == 36)
            {
                string[] symptoms_string = ls.symptoms_numbers.Split(' ');
                string[] reliability_string = ls.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ls.prevalence;

            }

            if (int.Parse(networkResult[3]) == 36)
            {
                string[] symptoms_string = ls.symptoms_numbers.Split(' ');
                string[] reliability_string = ls.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ls.prevalence;

            }

            if (int.Parse(networkResult[5]) == 36)
            {
                string[] symptoms_string = ls.symptoms_numbers.Split(' ');
                string[] reliability_string = ls.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ls.prevalence;

            }

            if (int.Parse(networkResult[1]) == 37)
            {
                string[] symptoms_string = ang.symptoms_numbers.Split(' ');
                string[] reliability_string = ang.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ang.prevalence;

            }

            if (int.Parse(networkResult[3]) == 37)
            {
                string[] symptoms_string = ang.symptoms_numbers.Split(' ');
                string[] reliability_string = ang.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ang.prevalence;

            }

            if (int.Parse(networkResult[5]) == 37)
            {
                string[] symptoms_string = ang.symptoms_numbers.Split(' ');
                string[] reliability_string = ang.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ang.prevalence;

            }

            if (int.Parse(networkResult[1]) == 38)
            {
                string[] symptoms_string = ot.symptoms_numbers.Split(' ');
                string[] reliability_string = ot.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = ot.prevalence;

            }

            if (int.Parse(networkResult[3]) == 38)
            {
                string[] symptoms_string = ot.symptoms_numbers.Split(' ');
                string[] reliability_string = ot.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = ot.prevalence;

            }

            if (int.Parse(networkResult[5]) == 38)
            {
                string[] symptoms_string = ot.symptoms_numbers.Split(' ');
                string[] reliability_string = ot.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = ot.prevalence;

            }

            if (int.Parse(networkResult[1]) == 39)
            {
                string[] symptoms_string = uea.symptoms_numbers.Split(' ');
                string[] reliability_string = uea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = uea.prevalence;

            }

            if (int.Parse(networkResult[3]) == 39)
            {
                string[] symptoms_string = uea.symptoms_numbers.Split(' ');
                string[] reliability_string = uea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = uea.prevalence;

            }

            if (int.Parse(networkResult[5]) == 39)
            {
                string[] symptoms_string = uea.symptoms_numbers.Split(' ');
                string[] reliability_string = uea.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = uea.prevalence;

            }

            if (int.Parse(networkResult[1]) == 40)
            {
                string[] symptoms_string = rs.symptoms_numbers.Split(' ');
                string[] reliability_string = rs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_1.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_1[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_1 = rs.prevalence;

            }

            if (int.Parse(networkResult[3]) == 40)
            {
                string[] symptoms_string = rs.symptoms_numbers.Split(' ');
                string[] reliability_string = rs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_2.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_2[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_2 = rs.prevalence;

            }

            if (int.Parse(networkResult[5]) == 40)
            {
                string[] symptoms_string = rs.symptoms_numbers.Split(' ');
                string[] reliability_string = rs.reliability_numbers.Split(' ');
                for (int i = 0; i < 226; i++)
                {
                    symp_str_3.Add(0);
                }
                for (int i = 0; i < symptoms_string.Length; i++)
                {
                    symp_str_3[int.Parse(symptoms_string[i]) - 1] = double.Parse(reliability_string[i]);
                }
                probability_3 = rs.prevalence;

            }

            double s = probability_1 + probability_2 + probability_3;
            probability_1 /= s;
            probability_2 /= s;
            probability_3 /= s;
            int best_index = -1;
            double best_proba = 0;
            for (int i = 0; i < 226; i++)
            {
                if (user_str[i] == 0)
                {
                    if ((symp_str_1[i] == 0 || symp_str_2[i] == 0 || symp_str_3[i] == 0) && (symp_str_1[i] > 0 || symp_str_2[i] > 0 || symp_str_3[i] > 0))
                    {
                        double p = probability_1 * symp_str_1[i];
                        p = Math.Max(p, probability_2 * symp_str_2[i]);
                        p = Math.Max(p, probability_3 * symp_str_3[i]);
                        if (p > best_proba)
                        {
                            best_proba = p;
                            best_index = i;
                        }
                    }
                }
            }
            int number = best_index + 1;

            return number;
        }

        protected async Task MakeCVDQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��������������� �� � ���",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�������� - ���������� �����������?",

                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeCDQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���� �� � ��� ����������� �����������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��������� ������ ���������� �� ������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��",
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeRDQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���� �� � ��� ������������ (��������, �������)",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });

            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ��������-����������� �������������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task SendFinalCard(int i1, int i2, int i3, double v1, double v2, double v3, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            List<string> names = new List<string>() { ie.title, pc.title, ta.title, hor.title, raa.title, aki.title, cs.title, ar.title, hyper.title, pc.title, hypoCys.title, hyperCys.title, sc.title, heartCys.title, md.title, rf.title, myo.title, pe.title, ph.title, ap.title, chd.title, mi.title, mvp.title, cm.title, per.title, vhd.title, chd.title, rchf.title, lchf.title, ca.title, ccv.title, col.title, lea.title, vv.title, end.title, ls.title, ang.title, ot.title, uea.title, rs.title };
            List<string> descriptions = new List<string>() { ie.description, pc.description, ta.description, hor.description, raa.description, aki.description, cs.description, ar.description, hyper.description, pc.description, hypoCys.description, hyperCys.description, sc.description, heartCys.description, md.description, rf.description, myo.description, pe.description, ph.description, ap.description, chd.description, mi.description, mvp.description, cm.description, per.description, vhd.description, chd.description, rchf.description, lchf.description, ca.description, ccv.description, col.description, lea.description, vv.description, end.description, ls.description, ang.description, ot.description, uea.description, rs.description };
            List<string> symptoms = new List<string>() { ie.symptoms, pc.symptoms, ta.symptoms, hor.symptoms, raa.symptoms, aki.symptoms, cs.symptoms, ar.symptoms, hyper.symptoms, pc.symptoms, hypoCys.symptoms, hyperCys.symptoms, sc.symptoms, heartCys.symptoms, md.symptoms, rf.symptoms, myo.symptoms, pe.symptoms, ph.symptoms, ap.symptoms, chd.symptoms, mi.symptoms, mvp.symptoms, cm.symptoms, per.symptoms, vhd.symptoms, chd.symptoms, rchf.symptoms, lchf.symptoms, ca.symptoms, ccv.symptoms, col.symptoms, lea.symptoms, vv.symptoms, end.symptoms, ls.symptoms, ang.symptoms, ot.symptoms, uea.symptoms, rs.symptoms };
            List<string> linkns = new List<string>() { ie.link, pc.link, ta.link, hor.link, raa.link, aki.link, cs.link, ar.link, hyper.link, pc.link, hypoCys.link, hyperCys.link, sc.link, heartCys.link, md.link, rf.link, myo.link, pe.link, ph.link, ap.link, chd.link, mi.link, mvp.link, cm.link, per.link, vhd.link, chd.link, rchf.link, lchf.link, ca.link, ccv.link, col.link, lea.link, vv.link, end.link, ls.link, ang.link, ot.link, uea.link, rs.link };
            string name1 = names[i1];
            string name2 = names[i2];
            string name3 = names[i3];
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�������� ��������� �����������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ������� ����� �����������������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���� ��������:",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            if (name1 == "������������� ����������� ��������� ���������������")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"������������� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"��������� ��������������� - {Math.Round(v1 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name1 == "�������������� ����������� ��������� ���������������")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"������������� �����������",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"��������� ��������������� - {Math.Round(v1 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
                else
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{name1} - \n{Math.Round(v1 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
            }

            if (name2 == "������������� ����������� ��������� ���������������")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"������������� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"��������� ��������������� - {Math.Round(v2 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name2 == "�������������� ����������� ��������� ���������������")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"������������� �����������",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"��������� ��������������� - {Math.Round(v2 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
                else
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{name2} - \n{Math.Round(v2 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
            }

            if (name3 == "������������� ����������� ��������� ���������������")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"������������� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"��������� ��������������� - {Math.Round(v3 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name3 == "�������������� ����������� ��������� ���������������")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"������������� �����������",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"��������� ��������������� - {Math.Round(v3 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
                else
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{name3} - \n{Math.Round(v3 * 100, 2)}%",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                }
            }
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = $"��� ������ �� �������� �������.",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = $"��� ���������� ����������� ��������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = $"������������� ���������� � �����.",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);

            SendFinalFinalCard(i1, i2, i3, turnContext, cancellationToken);
        }

        protected async Task SendFinalFinalCard(int i1, int i2, int i3, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            List<string> names = new List<string>() { ie.title, pc.title, ta.title, hor.title, raa.title, aki.title, cs.title, ar.title, hyper.title, pc.title, hypoCys.title, hyperCys.title, sc.title, heartCys.title, md.title, rf.title, myo.title, pe.title, ph.title, ap.title, chd.title, mi.title, mvp.title, cm.title, per.title, vhd.title, chd.title, rchf.title, lchf.title, ca.title, ccv.title, col.title, lea.title, vv.title, end.title, ls.title, ang.title, ot.title, uea.title, rs.title };
            List<string> descriptions = new List<string>() { ie.description, pc.description, ta.description, hor.description, raa.description, aki.description, cs.description, ar.description, hyper.description, pc.description, hypoCys.description, hyperCys.description, sc.description, heartCys.description, md.description, rf.description, myo.description, pe.description, ph.description, ap.description, chd.description, mi.description, mvp.description, cm.description, per.description, vhd.description, chd.description, rchf.description, lchf.description, ca.description, ccv.description, col.description, lea.description, vv.description, end.description, ls.description, ang.description, ot.description, uea.description, rs.description };
            List<string> symptoms = new List<string>() { ie.symptoms, pc.symptoms, ta.symptoms, hor.symptoms, raa.symptoms, aki.symptoms, cs.symptoms, ar.symptoms, hyper.symptoms, pc.symptoms, hypoCys.symptoms, hyperCys.symptoms, sc.symptoms, heartCys.symptoms, md.symptoms, rf.symptoms, myo.symptoms, pe.symptoms, ph.symptoms, ap.symptoms, chd.symptoms, mi.symptoms, mvp.symptoms, cm.symptoms, per.symptoms, vhd.symptoms, chd.symptoms, rchf.symptoms, lchf.symptoms, ca.symptoms, ccv.symptoms, col.symptoms, lea.symptoms, vv.symptoms, end.symptoms, ls.symptoms, ang.symptoms, ot.symptoms, uea.symptoms, rs.symptoms };
            List<string> links = new List<string>() { ie.link, pc.link, ta.link, hor.link, raa.link, aki.link, cs.link, ar.link, hyper.link, pc.link, hypoCys.link, hyperCys.link, sc.link, heartCys.link, md.link, rf.link, myo.link, pe.link, ph.link, ap.link, chd.link, mi.link, mvp.link, cm.link, per.link, vhd.link, chd.link, rchf.link, lchf.link, ca.link, ccv.link, col.link, lea.link, vv.link, end.link, ls.link, ang.link, ot.link, uea.link, rs.link };
            string name1 = names[i1];
            string name2 = names[i2];
            string name3 = names[i3];
            string finalString = "";

            finalString += $"{names[i1]} - {descriptions[i1]}\n";
            finalString += $"�������� ��������: {symptoms[i1]}\n";
            finalString += $"���������: {links[i1]}.\n";
            finalString += "\n";
            finalString += $"{names[i2]} - {descriptions[i2]}\n";
            finalString += $"�������� ��������: {symptoms[i2]}\n";
            finalString += $"���������: {links[i2]}.\n";
            finalString += "\n";
            finalString += $"{names[i3]} - {descriptions[i3]}\n";
            finalString += $"�������� ��������: {symptoms[i3]}\n";
            finalString += $"���������: {links[i3]}.\n";
            finalString += "\n";
            finalString += "��� �������� ������ ������� ��������� ������� ����� ���������.";

            await turnContext.SendActivityAsync(finalString);
        }

        protected async Task SendCard(int number, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (number == 0)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ����� ��� � ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 1)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� ��������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ���� ��� � ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 2)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���� � ����� ��� � �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ������ ��� ���������� ����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 3)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ����� ��� ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 4)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ����� ��� ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 5)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ����� ��� ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 6)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ����� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ����� ��� � ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 7)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 8)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ������ ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 9)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 10)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 11)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ��� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 12)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 13)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 14)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������ ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 15)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ���������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 16)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������ ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 17)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 18)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 19)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 20)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 21)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ���� � ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ������ ��� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 22)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 23)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 24)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 25)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 26)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 27)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ������ ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 28)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �� �� �������, ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ����� ���� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 29)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� � �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 30)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ � �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 31)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ��� ���������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ������� ������ � ������ ����� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 32)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ��� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 33)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 34)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ��� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��� ���������� ����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 35)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������, ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� �������� � ��������� ���� ��� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 36)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������, ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� �������� � ������� ���������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 37)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 38)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 39)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 40)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 41)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 42)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� �� ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 43)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ��� �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� � ��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 44)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 45)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 46)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 47)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �������� �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� � ����� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 48)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 49)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 50)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �������� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� � ����� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 51)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 52)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 53)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 54)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� ���� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 55)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 56)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 57)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 58)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 59)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�����, ��� ���� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 60)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 61)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � �������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 62)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� � ���������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 63)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 64)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 65)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 66)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �������� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� � ���������� ���������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 67)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���� � ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 68)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���� � ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 69)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 70)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ����� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 71)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ���������� � ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 72)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ���������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 73)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ��������� ���������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 74)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ������������ ������ ������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ������������ � �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 75)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������������� � ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 76)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 77)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� �� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����������� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 78)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 79)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �� � ��� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������, ��������� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 80)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �� � ��� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� � ���������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 81)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ���������� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 82)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 83)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��, ���,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ������ ������,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ������ ����� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 84)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 85)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 86)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 87)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ������� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� � ����� ������ ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 88)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ������� �������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� � ����� ������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 89)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 90)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 91)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 92)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 93)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 94)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 95)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 96)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 97)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 98)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 99)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 100)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 101)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 102)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = " ������ ��������� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 103)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 104)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 105)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 106)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 107)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 108)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 109)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 110)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 111)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 112)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 113)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 114)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 115)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 116)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 117)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� ���� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 118)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 119)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 120)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 121)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 122)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 123)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 124)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 125)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 126)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 127)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 128)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 129)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ���� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 130)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 131)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 132)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ���� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 133)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 134)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 135)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 136)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 137)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 138)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 139)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 140)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ���� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 141)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ���� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 142)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ���� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 143)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ���� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 144)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 145)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 146)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 147)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ����� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 148)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ������ ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 149)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �������� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 150)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 151)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 152)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ������, �������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 153)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 154)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ � ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 155)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 156)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 157)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ��� �� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 158)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 159)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ������ ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 160)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 161)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 162)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 163)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ���� ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 164)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 165)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ���� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 166)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 167)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 168)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� �������� ������ �����������,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 169)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ��������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 170)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�����, ��� ���� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������ ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 171)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�����, ��� ���� ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������ ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 172)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� � �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 173)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ � �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 174)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ��� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 175)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ���� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 176)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ���� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 177)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ���� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 178)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 179)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ���� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������� �� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 180)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 181)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ���� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 182)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� � ������� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������. ������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ������ �������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 183)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 184)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 185)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 186)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 187)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� ����� ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 188)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 189)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 190)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 191)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 192)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 193)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 194)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 195)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 196)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 197)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 198)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 199)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 200)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 201)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 202)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 203)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 204)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 205)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 206)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 207)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 208)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� �� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 209)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� � �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 210)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �� ��� ������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ������� �� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 211)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 212)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������������ ��������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 213)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������������������� ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 214)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������������������� �������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 215)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ���������������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ������ �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 216)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ���������������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 217)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ���������������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ������� �����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 218)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ���� � ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����� ���������������������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 219)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ����� � ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 220)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ����� � ����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 221)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� ��",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�� ������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 222)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� �� ���",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������� ������� �����?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 223)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������� ����������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 224)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� �� �� � ����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "� ��������� �����",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ��� ���?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }

            if (number == 225)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "����������� �� � ��� �",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��������� ����� ���������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������������?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "��",
                    Data = new AttachmentData().Type = "��"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "���",
                    Data = new AttachmentData().Type = "���"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�� ����",
                    Data = new AttachmentData().Type = "�� ����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }
        }

        protected async Task MakeAlcoholQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������������ �� �� ��������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��, ���������",
                Data = new AttachmentData().Type = "��, ���������"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��, �������� �����",
                Data = new AttachmentData().Type = "��, �������� �����"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��, �� �����",
                Data = new AttachmentData().Type = "��, �� �����"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeSmokeQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���� �� � ��� ���� � ������� ��� ������������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���� �������� ���������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���, �� ������ ����",
                Data = new AttachmentData().Type = "���, �� ������ ����"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeIDQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������ �� �� � ��������� 6 ������� �������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��� ������� ������������� �������������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���������� �������� �����?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��",
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�� ����",
                Data = new AttachmentData().Type = "�� ����"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeAllergyQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��������� �� ��� ������ �������� ��������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�� ����",
                Data = new AttachmentData().Type = "�� ����"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeInjuryQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�������� �� �� � ������� ��������� ��� �������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = " ������ �����, ����� ��� ������� ������?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "��",
                Data = new AttachmentData().Type = "��"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "���",
                Data = new AttachmentData().Type = "���"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�� ����",
                Data = new AttachmentData().Type = "�� ����"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeFoodQualityQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������� �������� ������ �������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�� ����� �� 0 �� 10",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveNumberInput
            {
                Id = "7",
                Min = 0,
                Max = 10,
                Value = 0
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeActivityQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������� ������� ����� ����������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "���������� �� ����� �� 0 �� 10",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveNumberInput
            {
                Id = "8",
                Min = 0,
                Max = 10,
                Value = 0
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeCVDList(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�����������, ���������� ��� ���������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ��� ��������-���������� �����������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "4"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeCDList(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�����������, ����������, ���������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ��� ����������� �����������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "5"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeIDList(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�����������, ����������, �������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "����������� ���� ������������ �����������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "6"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeAgeQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������� ��� ���?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveNumberInput
            {
                Id = "1",
                Min = 0,
                Max = 120,
                Value = 40
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeSexQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�������� ���� ���",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�������",
                Data = new AttachmentData().Type = "�������"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�������",
                Data = new AttachmentData().Type = "�������"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeWeightQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "����� � ��� ���?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveNumberInput
            {
                Id = "2",
                Min = 20,
                Max = 200,
                Value = 70
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeHeightQuestion(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "����� � ��� ����?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveNumberInput
            {
                Id = "3",
                Min = 110,
                Max = 230,
                Value = 170
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeSymptomsList(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�����������, ����������, ��������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������� ��� ���������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "9",
                Value = "���� � ������, ������, ���� ��� � �.�."
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task AddSymptoms(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            string end = ",";
            for (int i = 1; i < symptomsList.Count + 1; i++)
            {
                if (i == symptomsList.Count)
                {
                    end = ".";
                }
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"{symptomsNames[symptomsList[i - 1]]}{end}",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                }); ;
            }
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������������ ������������ ��� �����",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������ ��������������� � ���������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��� ����� ������ �������������� � �������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "10",
                Value = ""
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task DeleteSymptoms(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            string end = ",";
            for (int i = 1; i < symptomsList.Count + 1; i++)
            {
                if (i == symptomsList.Count)
                {
                    end = ".";
                }
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"{symptomsNames[symptomsList[i - 1]]}{end}",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                }); ;
            }
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "�������, ����������, ��������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������� ���������� ������� �� ������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "11",
                Value = ""
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "�����",
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected async Task MakeCorrections1(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Value == null || turnContext.Activity.Value.ToString().Split(':').Length < 2)
            {
                message_number = 17;
                await turnContext.SendActivityAsync("�������, ����������, ������ ��������� � ����, ����������� ��� ������� '�����'.");
            }
            else
            {
                SC.Controllers.SymptomsList sl = new Controllers.SymptomsList(turnContext.Activity.Value.ToString().Split(':')[1].Replace('}', ' ').Trim());
                sl.SplitText();
                sl.RecognizeIntents();
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "��� ������� ���������� ��������� ��������:",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                string end = ",";
                for (int i = 1; i < symptomsList.Count + 1; i++)
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{symptomsNames[symptomsList[i - 1]]}{end}",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    }); ;
                }
                int prevCount = symptomsList.Count;
                for (int i = 1; i < sl.SymptomList.Count + 1; i++)
                {
                    symptomsList.Add(sl.SymptomList[i - 1]);
                    if (i == sl.SymptomList.Count)
                    {
                        end = ".";
                    }
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{symptomsNames[symptomsList[i + prevCount - 1]]}{end}",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    }); ;
                }

                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������, �� �� ���������, � ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ��� ������ ��������,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ���-�� �� ���.",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�������� ��������",
                    Data = new AttachmentData().Type = "�������� ��������"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "������ ��������",
                    Data = new AttachmentData().Type = "������ ��������"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�����",
                    Data = new AttachmentData().Type = "�����"
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }
        }

        protected async Task MakeCorrections2(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Value == null || turnContext.Activity.Value.ToString().Split(':').Length < 2)
            {
                message_number = 18;
                await turnContext.SendActivityAsync("�������, ����������, ������ ��������� � ����, ����������� ��� ������� '�����'.");
            }
            else
            {
                Controllers.SymptomsList sl = new SC.Controllers.SymptomsList(turnContext.Activity.Value.ToString().Split(':')[1].Replace('}', ' ').Trim());
                sl.SplitText();
                sl.RecognizeIntents();
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "������ ������ ��������� �������� ���:",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                string end = ",";
                for (int i = 1; i < sl.SymptomList.Count + 1; i++)
                {
                    symptomsList.Remove(sl.SymptomList[i - 1]);
                }
                for (int i = 1; i < symptomsList.Count + 1; i++)
                {
                    if (i == symptomsList.Count)
                    {
                        end = ".";
                    }
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"{symptomsNames[symptomsList[i - 1]]}{end}",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    }); ;
                }

                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���������, �� �� ���������, � ����������",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "�������� ��� ������ ��������,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "���� ���-�� �� ���.",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�������� ��������",
                    Data = new AttachmentData().Type = "�������� ��������"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "������ ��������",
                    Data = new AttachmentData().Type = "������ ��������"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "�����",
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };

                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }
        }
        protected async Task CreateStartingCard(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "������������!",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ������� ������� ���-���� �� ������ ������,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "��������������� �� ��������� � ��� ��������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "� ������� �������� - ���������� �����������",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        public string SendToNetwork(List<double> symptomsString)
        {
            string message = "";
            for (int i = 0; i < symptomsString.Count; i++)
            {
                message += symptomsString[i].ToString();
                if (i != symptomsString.Count - 1)
                {
                    message += " ";
                }
            }
            return Clases.VKBot.Authorization(message);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id && message_number == 0)
                {
                    //await CreateStartingCard(turnContext, cancellationToken);
                }
            }
        }
    }
}
