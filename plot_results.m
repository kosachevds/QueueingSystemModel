data = dlmread('temp.txt')';
row_count = size(data, 1);
x_array = data(1, :);
data = data(2:row_count, :);
downtimes = data(3, :);
data(3, :) = [];

plot(x_array, data);
legend('��. ����� ����������', '��. ����� ��������', ...
    '���������� ������ �������', '��. ������ �������');

figure;
loglog(x_array, downtimes);
title('��. ����� �������');
