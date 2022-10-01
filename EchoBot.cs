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
        private static List<string> symptomsNames = new List<string>() { "боль за грудиной или в области сердца", "внезапная боль за грудиной или в области сердца", "боль за грудиной или в области сердца при физической активности", "колющая боль за грудиной или в области сердца", "тянущая боль за грудиной или в области сердца", "давящая боль за грудиной или в области сердца", "тупая боль за грудиной или в области сердца", "боль в животе", "пульсация в районе живота", "боль в пояснице", "боль в шее", "боль при жевании", "боль в глазах", "боль в мышцах", "боль в мыщцах ног", "боль в икроножных мышцах", "боль в мышцах рук", "боль в суставах", "боль в пальцах", "боль в нижних конечностях", "ноющая боль в нижних конечностях", "боль в нижних конечностях при ходьбе", "боль в пальцах ног", "боль в верхних конечностях", "боль в пальцах рук", "учащённое сердцебиение", "замедленное сердцебиение", "перебои в работе сердца", "слабость пульса", "ощущение стеснения в груди", "жжение в груди", "иррадиация боли из грудной клетки в другие части тела", "одышка", "одышка появляется внезапно", "одышка при физической активности", "ортопноэ", "одышка в положении сидя или стоя", "учащение дыхания", "замедление дыхания", "тошнота", "рвота", "чувство тревоги", "слабость", "усталость", "онемение лица", "онемение пальцев", "онемение верхних конечностей", "онемение верхних конечностей с одной стороны", "онемение пальцев рук", "онемение нижних конечностей", "онемение нижних конечностей с одной стороны", "онемение пальцев ног", "отёк лёгких", "отёк лица", "отёки на коже головы", "отёк шеи", "отёки нижних конечностей", "отёки верхних конечностей", "повышенное потоотделение", "холодный пот", "головная боль", "головная боль в височной части", "головная боль в затылочной части", "пульсирующая головная боль", "сдавливающая головная боль", "головокружение", "обмороки или обморочное состояние", "шум в ушах", "шум в сердце", "ухeдшение зрения", "видения перед глазами", "потемнение в глазах", "расширение зрачков", "нарушение сознания", "потеря ориентировки во времени и пространстве", "заторможенность", "рассеянность внимания", "апатия", "нарушение сна", "нарушение речи", "проблемы с глотанием", "психомоторное возбуждение", "шаткость походки", "неустойчивость", "ограничение двигательных функций", "ограничение двигательных функций нижних конечностей", "ограничение двигательных функций верхних конечностей", "ограничение двигательных функций нижних конечностей с одной стороны тела", "ограничение двигательных функций верхних конечностей с одной стороны тела", "чувство жара", "озноб", "зябкость пальцев", "зябкость нижних конечностей", "зябкость пальцев ног", "зябкость верхних конечностей", "зябкость пальцев рук", "повышение температуры", "низкая температура кожи", "низкая температура пальцев", "низкая температура ног", "низкая температура пальцев ног", "низкая температура рук", "низкая температура пальцев рук", "повышение давления", "понижение давления", "частые перепады настроения", "покраснение кожи", "покраснение лица", "покраснение пальцев", "покраснение нижних конечностей", "покраснение пальцев ног", "покраснение верхних конечностей", "покраснение пальцев рук", "покраснение ушей", "покраснение губ", "покраснение носа", "покраснение глаз", "покраснение кожи головы", "бледность кожи", "бледность лица", "бледность пальцев", "бледность нижних конечностей", "бледность пальцев ног", "бледность верхних конечностей", "бледность пальцев рук", "бледность ушей", "бледность губ", "бледность носа", "синюшность кожи", "цианоз нижних конечностей", "цианоз пальцев", "цианоз пальцев ног", "цианоз верхних конечностей", "цианоз пальцев рук", "цианоз лица", "цианоз ушей", "цианоз губ", "цианоз носа", "желтизна кожи", "желтизна лица", "сухость кожи", "сухость кожи лица", "сухость кожи ног", "сухость кожи рук", "влажность кожи", "носовые кровотечения", "внутренние кровотечения", "набор веса", "потеря веса", "дефицит веса", "потеря аппетита", "кашель", "кашель усиливается в положении лёжа", "сухой кашель", "мокрота", "хрипы", "кровохарканье", "набухание вен на шее", "увеличение печени", "боль в правом подреберье", "вздутие живота", "появление внутренних язв", "появление язв на пальцах", "появление язв на нижних конечностях", "появление язв на пальцах ног", "появление язв на верхних конечностях", "появление язв на пальцах рук", "судороги в нижних конечностях", "судороги в нижних конечностях беспокоят по ночам", "судороги в верхних конечностях", "быстрая утомляемость ног", "быстрая утомляемость рук", "тяжесть в ногах", "жжение в ногах", "зуд кожи", "зуд кожи лица", "зуд кожи рук", "зуд кожи ног", "сосудистые звёздочки", "сосудистые звёздочки на коже", "сосудистые звёздочки на ногах", "сосудистые звёздочки на руках", "ассиметричная улыбка", "потемнение кожи", "потемнение нижних конечностей", "потемнение пальцев", "потемнение пальцев ног", "потемнение верхних конечностей", "потемнение пальцев рук", "потемнение лица", "потемнение ушей", "потемнение губ", "потемнение носа", "ломкость волос", "выпадение волос", "выпадение волос на руках", "выпадение волос на ногах", "жажда", "икота", "олигурия", "тремор", "тремор нижних конечностей", "тремор верхних конечностей", "тремор пальцев", "тремор пальцев ног", "тремор пальцев рук", "сыпь", "сыпь на руках", "сыпь на ногах", "проблемы с памятью", "болячки на голове", "импотенция", "затруднённое мочеиспускание", "гиперчувствительность кожи", "гиперчувствительность пальцев", "гиперчувствительность кожи нижних конечностей", "гиперчувствительность пальцев ног", "гиперчувствительность кожи верхних конечностей", "гиперчувствительность пальцев рук", "белок в моче", "кровь в моче", "диарея", "жидкий стул", "сонливость", "аммиачный запах изо рта", "гипергликемия" };
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
            if (turnContext.Activity.Text!= null && turnContext.Activity.Text.ToLower() == "!вначало")
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
                if (turnContext.Activity.Text != null && turnContext.Activity.Text.ToLower() == "!ксимптомам")
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
                            if (turnContext.Activity.Text == "Да")
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
                            if (turnContext.Activity.Text == "Да")
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
                            if (turnContext.Activity.Text == "Да")
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
                            if (turnContext.Activity.Text == "Добавить симптомы")
                            {
                                await AddSymptoms(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number = 18;
                                break;
                            }
                            else
                            {
                                if (turnContext.Activity.Text == "Убрать симптомы")
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
                                    await turnContext.SendActivityAsync("Сейчас я задам несколько специальных вопросов по вашим симптомам. Если вы готовы, напишите какое-нибудь сообщение.");
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
                            if (turnContext.Activity.Text == "Добавить симптомы")
                            {
                                await AddSymptoms(turnContext, cancellationToken);
                                answer_number = message_number;
                                message_number = 18;
                                break;
                            }
                            else
                            {
                                if (turnContext.Activity.Text == "Убрать симптомы")
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
                                    await turnContext.SendActivityAsync("Сейчас я задам несколько специальных вопросов по вашим симптомам. Если вы готовы, напишите какое-нибудь сообщение.");
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
                            if (actual_number != -1 && turnContext.Activity.Text == "Нет")
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
                                    await turnContext.SendActivityAsync("Это был последний вопрос. Отправьте какое-нибудь сообщение, чтобы перейти к результатам.");
                                    answer_number = message_number;
                                    message_number = 24;
                                }
                            }
                            break;
                        case 23:
                            bool successFlag = false;
                            if (turnContext.Activity.Text == "Да")
                            {
                                currentSymptoms.RecognizeTrueSymptom(actual_number);
                                successFlag = true;
                            }
                            if (turnContext.Activity.Text == "Нет")
                            {
                                currentSymptoms.RecognizeNegativeSymptom(actual_number);
                                successFlag = true;
                            }
                            if (turnContext.Activity.Text == "Не знаю")
                            {
                                currentSymptoms.RecognizeProbableSymptom(actual_number);
                                successFlag = true;
                            }
                            if (!successFlag)
                            {
                                await turnContext.SendActivityAsync("Дайте, пожалуйста, один из вариантов ответа, указанных на карточке с вопросом");
                                break;
                            }
                            await turnContext.SendActivityAsync("Вы уверены?");
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
                Text = "Диагностированы ли у вас",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "сердечно - сосудистые заболевания?",

                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
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
                Text = "Есть ли у вас хронические заболевания,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "способные давать осложнения на сердце?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да",
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
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
                Text = "Есть ли у вас родственники (возможно, умершие)",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });

            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "с сердечно-сосудистыми заболеваниями?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
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
                Text = "Наиболее вероятные заболевания,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "о которых могут свидетельствовать",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "ваши симптомы:",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            if (name1 == "Левосторонняя хроническая сердечная недостаточность")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"Левосторонняя хроническая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"сердечная недостаточность - {Math.Round(v1 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name1 == "Правосторонняя хроническая сердечная недостаточность")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"Правостороняя хроническая",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"сердечная недостаточность - {Math.Round(v1 * 100, 2)}%",
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

            if (name2 == "Левосторонняя хроническая сердечная недостаточность")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"Левосторонняя хроническая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"сердечная недостаточность - {Math.Round(v2 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name2 == "Правосторонняя хроническая сердечная недостаточность")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"Правостороняя хроническая",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"сердечная недостаточность - {Math.Round(v2 * 100, 2)}%",
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

            if (name3 == "Левосторонняя хроническая сердечная недостаточность")
            {
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"Левосторонняя хроническая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = $"сердечная недостаточность - {Math.Round(v3 * 100, 2)}%",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            }
            else
            {
                if (name3 == "Правосторонняя хроническая сердечная недостаточность")
                {
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"Правостороняя хроническая",
                        Size = AdaptiveTextSize.Medium,
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                    });
                    card.Body.Add(new AdaptiveTextBlock
                    {
                        Text = $"сердечная недостаточность - {Math.Round(v3 * 100, 2)}%",
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
                Text = $"Эти данные не являются точными.",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = $"Для постановки конкретного диагноза",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = $"рекомендуется обратиться к врачу.",
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
            finalString += $"Основные симптомы: {symptoms[i1]}\n";
            finalString += $"Подробнее: {links[i1]}.\n";
            finalString += "\n";
            finalString += $"{names[i2]} - {descriptions[i2]}\n";
            finalString += $"Основные симптомы: {symptoms[i2]}\n";
            finalString += $"Подробнее: {links[i2]}.\n";
            finalString += "\n";
            finalString += $"{names[i3]} - {descriptions[i3]}\n";
            finalString += $"Основные симптомы: {symptoms[i3]}\n";
            finalString += $"Подробнее: {links[i3]}.\n";
            finalString += "\n";
            finalString += "Для указания нового перечня симптомов введите любое сообщение.";

            await turnContext.SendActivityAsync(finalString);
        }

        protected async Task SendCard(int number, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (number == 0)
            {
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "Беспокоит ли вас боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груди или в области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникают ли у вас внезапные боли",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груи или в области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникает ли боль в груди или в области",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сердца только при физической активности?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас колющая боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груди или области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас тянущая боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груди или области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас давящяя боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груди или области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас тупая боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в груди или в области сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в животе?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Ощущаете ли вы пульсации",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в районе живота?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в пояснице?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в шее?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль при жевании?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в глазах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в мышцах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в мышцах ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в икроножных мышцах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в мышцах рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в суставах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в пальцах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в нижних конечностях?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ноющая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в нижних конечностях?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас боль в нижних",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "конечностях только при ходьбе?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в пальцах ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в верхних конечностях?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в пальцах рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "учащённое сердцебиение?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "замедленное сердцебиение?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Ощущаете ли вы перебои",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в работе сердца?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Можете ли вы сказать, что",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "ваш пульс едва заметен?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Ощущаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "стеснение в груди?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Ощущаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "жжение в груди?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Происходит ли у вас иррадиация боли",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "из грудной клетки в другие части тела?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас одышка?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас внезапно",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "появляющаяся одышка?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появляется ли у вас одышка",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "только при физической активности?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас одышка, исчезающая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "при переходе в положение сидя или стоя?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас одышка, исчезающая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "при переходе в лежачее положение?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "учащение дыхания?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "замедление дыхания?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Тошнит ли вас?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Была ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время рвота?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "чувство тревоги?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Чувствуете ли вы слабость?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас чувство",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "усталости и беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "быстрая утомляемость?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "онемение лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "онемение пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "онемение верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас онемение верхних",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "конечностей с одной стороны?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "онемение пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас онемение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас онемение нижних",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "конечностей с одной стороны?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "онемение пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёк лёгких?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёк лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёки на коже головы?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёк шеи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёки нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "отёки верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенное потоотделение?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время, что ваша кожа",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покрывается холодным потом?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "головная боль?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас головная",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в височной части?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас головная",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "боль в затылочной части?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас пульсирующая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "головная боль?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас сдавливающая",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "головная боль?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Имеется ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "головокружение?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Случались ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время обмороки или",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "переходы в обморочное состояние?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие шума в ушах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ощущение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие шума в сердце?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "ухудшение зрения?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас частое",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "появление видений перед глазами?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоило ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "внезапное потемнение в глазах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "чрезмерное расширение зрачков?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Случалось ли у вас в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время нарушение сознанния?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Случалась ли у вас в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время беспричинная потеря ориентировки",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в пространстве и времени?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы некую",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "заторможенность в своих действиях",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "рассеянность внимания?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечаете ли вы за собой",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "некоторую апатичность в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "регулярное нарушение сна?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Имеются ли у вас сейчас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "проблемы, связанные с",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспричинным нарушением речи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Имеются ли у вас сейчас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "проблемы с глотанием?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Имеются ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "ярко выраженные признаки",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "психомоторного возбуждения?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдается ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "шаткость походки?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Правда ли, что,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "попытавшись сейчас встать,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "вы можете легко упасть?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ограничение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "двигательных функций?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ограничение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "двигательных функций",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ограничение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "двигательных функций",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ограничение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "двигательных функций нижних",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "конечностей с одной строны тела?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас ограничение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "двигательных функций верхних",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "конечностей с одной стороны тела?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Есть ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "чувство жара?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас озноб?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас зябкость пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас зябкость",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли зябкость",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас зябкость",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас зябкость",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдается ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "поышенная температура?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое понижение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "температуры кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое понижение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "температуры пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое понижение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "температуры ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое пожинение температуры",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое понижение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "температуры рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = " резкое понижение температуры",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечалось ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенное давление",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечалось ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пониженное давление?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время частые",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "перепады настроения?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время покраснение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время покраснение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоило ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время покраснение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение ушей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение губ?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение носа?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время внезапное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение глаз?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "покраснение кожи головы?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенная бледность кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенная бледность лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "бледность пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время бледность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "бледность пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время бледность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "бледность пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенная бледность ушей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенная бледность губ?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенная бледность носа?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "синюшность кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время посинение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "участков нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое посинение пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "посинение пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время посинение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "участков верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "посинение пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое посинение лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое посинение ушей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое посинение губ?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкое посинение носа?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время повышение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "желтизны кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время повшение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "желтизны лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "у себя повышенную",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сухость кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "у себя повышенную",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сухость кожи лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "у себя повышенную",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сухость кожи ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдаете ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "у себя повышенную",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сухость кожи рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспричинное повышение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "влажности кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Случались ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "носовые кровотечения?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Случались ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "внутренние кровотечения?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкий набор веса?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "резкую потерю веса?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечаете ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "появление дефицита веса",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспричинная",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потеря аппетита?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "кашель?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время кашель, усиливающийся",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в положении лежа?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сухой кашель?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "кашель с мокротой?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Отмечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие хрипов?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоило ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "кровохарканье?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "набухание вен на шее?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "увеличение печени?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас боль",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в правом подреберье",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоило ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "внезапное вздутие живота?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "внутренние язвы?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "язвы на пальцах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время язвы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "на коже нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "язвы на пальцах ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время язвы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "на коже верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялись ли у вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "язвы на пальцах рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдались ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время судороги",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдались ли у вас в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время судороги нижних конечностей,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспокоящие по ночам?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдались ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время судороги",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время, что ваши ноги",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "слишком быстро устают?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время, что ваши руки",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "слишком быстро устают?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Чувствовали ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "тяжесть в ногах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Ощущали ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "жжение в ногах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "вас зуд кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "зуд кожи лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "зуд кожи рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "зуд кожи ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время появление",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сосудистых звёздочек?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время появление",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сосудистых звёздочек на коже?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время появление",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сосудистых звёздочек на ногах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время появление",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сосудистых звёздочек на руках?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Посмотрите в зеркало и",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "улыбнитесь. Кажется ли вам",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "ваша улыбка ассиметричной?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время потемнение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "участков нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время потемнение",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "участков верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение лица?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение ушей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение губ?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "потемнение носа?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенную ломкость волос?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "чрезмерное выпадение волос?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "выпадение волос на руках?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "выпадение волос на ногах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Часто ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспокоит жажда?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Часто ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "беспокоит икота?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалась ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "олигурия?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "тремор?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время тремор",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоил ли вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время тремор",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникал ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "тремор пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникал ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "тремор пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникал ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "тремор пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялась ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сыпь?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялась ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сыпь на руках?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Появлялась ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "сыпь на ногах?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Возникали ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "проблемы с памятью?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоит ли вас беспричинное",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "появление болячек на голове?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Страдали ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "от импотенции?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "затрудненное мочеиспускание?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "гиперчувствительность кожи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "гиперчувствительность пальцев?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время гиперчувствительность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "кожи нижних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время гиперчувствительность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев ног?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время гиперчувствительность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "кожи верхних конечностей?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя в последнее",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "время гиперчувствительность",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "пальцев рук?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие белка в моче?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие крови в моче?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Страдали ли вы",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "от диареи?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Беспокоило ли вас",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "наличие жидкого стула?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "повышенную сонливость?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Замечали ли вы у себя",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "в последнее время",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "аммиачный запах изо рта?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                    Text = "Наблюдалось ли у вас в",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "последнее время состояние",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "гипергликемии?",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Да",
                    Data = new AttachmentData().Type = "Да"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Нет",
                    Data = new AttachmentData().Type = "Нет"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Не знаю",
                    Data = new AttachmentData().Type = "Не знаю"
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
                Text = "Употребляете ли вы алкоголь?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да, регулярно",
                Data = new AttachmentData().Type = "Да, регулярно"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да, довольно часто",
                Data = new AttachmentData().Type = "Да, довольно часто"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да, но редко",
                Data = new AttachmentData().Type = "Да, но редко"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
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
                Text = "Есть ли у вас тяга к курению или употреблению",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "иной табачной продукции?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет, но раньше была",
                Data = new AttachmentData().Type = "Нет, но раньше была"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
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
                Text = "Болели ли вы в последние 6 месяцев ковидом",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "или другими инфекционными заболеваниями,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "способными поражать лёгкие?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да",
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Не знаю",
                Data = new AttachmentData().Type = "Не знаю"
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
                Text = "Беспокоит ли вас сейчас сезонная аллергия?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Не знаю",
                Data = new AttachmentData().Type = "Не знаю"
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
                Text = "Получали ли вы в течение последних трёх месяцев",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = " травмы почек, спины или грудной клетки?",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Да",
                Data = new AttachmentData().Type = "Да"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Нет",
                Data = new AttachmentData().Type = "Нет"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Не знаю",
                Data = new AttachmentData().Type = "Не знаю"
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
                Text = "Оцените качество вашего питания",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "по шкале от 0 до 10",
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
                Title = "Далее",
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
                Text = "Оцените уровень вашей физической",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "активности по шкале от 0 до 10",
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
                Title = "Далее",
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
                Text = "Перечислите, пожалуйста уже имеющиеся",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "у вас сердечно-сосудистые заболевания",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "4"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Далее",
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
                Text = "Перечислите, пожалуйста, имеющиеся",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "у вас хронические заболевания",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "5"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Далее",
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
                Text = "Перечислите, пожалуйста, недавно",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "перенесённые вами инфецкионные заболевания",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "6"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Далее",
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
                Text = "Сколько вам лет?",
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
                Title = "Далее",
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
                Text = "Выберите свой пол",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Мужской",
                Data = new AttachmentData().Type = "Мужской"
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Женский",
                Data = new AttachmentData().Type = "Женский"
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
                Text = "Какой у вас вес?",
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
                Title = "Далее",
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
                Text = "Какой у вас рост?",
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
                Title = "Далее",
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
                Text = "Перечислите, пожалуйста, симптомы,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "которые вас беспокоят",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextInput
            {
                Id = "9",
                Value = "Боль в голове, кашель, отёки ног и т.д."
            });
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Title = "Далее",
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
                Text = "Постарайтесь использовать как можно",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "больше существительных и указывать",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "как можно меньше прилагательных и деталей",
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
                Title = "Далее",
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
                Text = "Введите, пожалуйста, симптомы,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "которые необходимо удалить из списка",
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
                Title = "Далее",
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
                await turnContext.SendActivityAsync("Введите, пожалуйста, список симптомов в поле, размещённое над кнопкой 'Далее'.");
            }
            else
            {
                SC.Controllers.SymptomsList sl = new Controllers.SymptomsList(turnContext.Activity.Value.ToString().Split(':')[1].Replace('}', ' ').Trim());
                sl.SplitText();
                sl.RecognizeIntents();
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "Мне удалось распознать следующие симптомы:",
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
                    Text = "Проверьте, всё ли правильно, и попробуйте",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "добавить или убрать симптомы,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "если что-то не так.",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Добавить симптомы",
                    Data = new AttachmentData().Type = "Добавить симптомы"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Убрать симптомы",
                    Data = new AttachmentData().Type = "Убрать симптомы"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Далее",
                    Data = new AttachmentData().Type = "Далее"
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
                await turnContext.SendActivityAsync("Введите, пожалуйста, список симптомов в поле, размещённое над кнопкой 'Далее'.");
            }
            else
            {
                Controllers.SymptomsList sl = new SC.Controllers.SymptomsList(turnContext.Activity.Value.ToString().Split(':')[1].Replace('}', ' ').Trim());
                sl.SplitText();
                sl.RecognizeIntents();
                AdaptiveCard card = new AdaptiveCard();
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "Теперь список симптомов выглядит так:",
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
                    Text = "Проверьте, всё ли правильно, и попробуйте",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "добавить или убрать симптомы,",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Body.Add(new AdaptiveTextBlock
                {
                    Text = "если что-то не так.",
                    Size = AdaptiveTextSize.Medium,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Добавить симптомы",
                    Data = new AttachmentData().Type = "Добавить симптомы"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Убрать симптомы",
                    Data = new AttachmentData().Type = "Убрать симптомы"
                });
                card.Actions.Add(new AdaptiveSubmitAction
                {
                    Title = "Далее",
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
                Text = "Здравствуйте!",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "С помощью данного чат-бота вы можете узнать,",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "свидетельствуют ли имеющиеся у вас симптомы",
                Size = AdaptiveTextSize.Medium,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Center
            });
            card.Body.Add(new AdaptiveTextBlock
            {
                Text = "о наличии сердечно - сосудистых заболеваний",
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
