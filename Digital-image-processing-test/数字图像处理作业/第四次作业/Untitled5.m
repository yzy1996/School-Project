picture = imread('test1.pgm');

picture1 = im2double(picture);
picture2 = double(picture);

picture11=picture1*2;
picture22=picture2*2;

picture111=im2uint8(picture11);
picture222=uint8(picture22);

if(picture111==picture222)
    a=1;
else
    a=0;
end