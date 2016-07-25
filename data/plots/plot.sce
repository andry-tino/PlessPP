// Clean up
clear
clc

function plotFolder(folder, windowNr)
    cd(folder)
    // Get the data from the files
    files = listfiles(folder)
    data = list()
    for i = 1:size(files, 1)
        data(i) = strtod(read_csv(files(i)))
    end
    
    // Combine the data
    maxSize = 0
    for i = 1:size(data)
        maxSize = max(maxSize, size(data(i), 1))
    end
    accelerationsX = zeros(maxSize, size(data))
    for i = 1:size(data)
        accelerationsX(1:size(data(i), 1), i) = data(i)(:, 1)
    end
    accelerationsY = zeros(maxSize, size(data))
    for i = 1:size(data)
        accelerationsY(1:size(data(i), 1), i) = data(i)(:, 2)
    end
    accelerationsZ = zeros(maxSize, size(data))
    for i = 1:size(data)
        accelerationsZ(1:size(data(i), 1), i) = data(i)(:, 3)
    end
    
    // Create a window number 1
    xset('window',windowNr)
    
    // Plot the data
    param3d1(accelerationsX, accelerationsY, list(accelerationsZ, 1:size(data)))
endfunction

plotFolder('C:\Repos\PlessPP\data\Positive\Jeroen Positive', 1)
plotFolder('C:\Repos\PlessPP\data\Negative\Jeroen Negative', 2)
