import numpy as np
import random

# ���� ������ � ���������� ������� ���������, ����������� ��� �����������,
# ������� ������������� ������� ������������ �������� ��� ������� �����������,
# ������������ ������������ ����������� ������� �� ����������� ��������� �������������.
input_data = np.array (list(map(int, input().split()))) - 1
occurrencies = np.array (list(map(int, input().split())))
expected_reliabilities = np.array (list(map(int, input().split()))) / 100

# ���������� ����� ����� ��������, ������� ���������� ��������� ���������� ���������� ��� ������� ��������.
# ���������� ����� �������, ����� � �������� ����������� ��� ��������, ����������� ��� �����������.
symptoms_count = len (input_data)
probability_of_abcent = -occurrencies + 100
real_maximum = 1000 - np.prod (probability_of_abcent / 100) * 1000
cs_strings_count = max (1, int ((np.prod (occurrencies / 100)) * 1000 / real_maximum * 1000))
occurrencies = occurrencies * 10 / real_maximum * 1000
occurrencies = occurrencies.astype(int) - cs_strings_count
remain_strings_count = 1000 - cs_strings_count

# ����������� ������� ��������� ������������ �����, �������� ���������� ��������� �������.
expected_reliabilities_table = [[0 for t in range (occurrencies[i])] for i in range (len (input_data))]
for i in range (len (input_data)):
    for t in range (occurrencies[i]):
        expected_reliabilities_table[i][t] = random.uniform(expected_reliabilities[i] - min (expected_reliabilities[i] - 0.5, 1 - expected_reliabilities[i]), expected_reliabilities[i] + min (expected_reliabilities[i] - 0.5, 1 - expected_reliabilities[i]))
    random.shuffle (expected_reliabilities_table[i])

# ���������� �������� (data_table) �������� ����������� � �������� ����� ������� ��������� ���������.
data_table = [[0 for i in range (227)] for i in range (remain_strings_count)]
for t in range (len (input_data)):
    for i in range (len (data_table)):
        data_table[i][0] = np.sum (np.array (data_table[i])) - data_table[i][0]
    data_table.sort()
    for k in range (occurrencies[t]):
        data_table[k][input_data[t] + 1] = expected_reliabilities_table[t][k]
        
# ���������� � ������� �������, ����� � �������� ����������� ��� ��������, ����������� ��� �����������.
add_data_table = [[0 for i in range (227)] for i in range (cs_strings_count)]
for i in range (cs_strings_count):
    for j in range (len (occurrencies)):
        add_data_table[i][input_data[j] + 1] = random.uniform(expected_reliabilities[j] - min (expected_reliabilities[j] - 0.5, 1 - expected_reliabilities[j]), expected_reliabilities[j] + min (expected_reliabilities[j] - 0.5, 1 - expected_reliabilities[j]))
data_table += add_data_table

# �������� ������ � ����.
file = open("dataset.txt", "a")
for i in range (len (data_table)):
    file.write (" ".join(map(str, data_table[i][1:]))+'\n')
file.close()
    

