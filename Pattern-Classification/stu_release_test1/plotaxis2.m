function plotaxis2
x = get(gca,'XLim');
y = get(gca,'YLim');
xx = (x(2)-x(1))/50;
yy = (y(2)-y(1))/50;
ox = (x(2)+x(1))/2;
oy = (y(2)+y(1))/2;
right = true;
up = true;
hold on
    if ox == 0
        cox = 0;
        right = true;
    end
    if ox > 0
        right = true;
        if x(1)<0
            cox = 0;
        else
            cox = x(1);
        end
    end
    if ox < 0
        right = false;
        if x(2)>0
            cox = 0;
        else
            cox = x(2);
        end
    end
    
    if oy == 0
        coy = 0;
        up = true;
    end
    if oy > 0
        up = true;
        if y(1)<0
            coy = 0;
        else
            coy = y(1);
        end
    end
    if oy < 0
        up = false;
        if x(2)>0
            coy = 0;
        else
            coy = y(2);
        end
    end
    if xx*50>=2
        xall = 1;
    else
        xall = xx*20;
    end
    if yy*50>=2
        yall = 1;
    else
        yall = yy*20;
    end

if right&&up
    plot([cox-xall/2,cox+xall],[coy,coy],'k')
    plot([cox,cox],[coy-xall/2,yall+coy],'k')
    
    plot([cox,cox+xx],[yall+coy,yall+coy-yy],'k')
    plot([cox-xx,cox],[coy+yall-yy,coy+yall],'k')
    
    plot([cox+xall-xx,xall+cox],[yy+coy,coy],'k')
    plot([cox+xall-xx,xall+cox],[coy-yy,coy],'k')

    text(cox,coy+yy*1.3,['(' num2str(cox) ',' num2str(coy) ')']);
    text(cox+xall,coy+yy*1.3,['x(' num2str(cox+xall) ',' num2str(coy) ')']);
    text(cox,coy+yall+yy*1.3,['y(' num2str(cox) ',' num2str(coy+yall) ')']);
end
if ~right&&up
    plot([cox-xall,cox+xall/2],[coy,coy],'k')%--------向左
    plot([cox,cox],[coy-yall/2,yall+coy],'k')%-------向上
    
    plot([cox,cox+xx],[yall+coy,yall+coy-yy],'k')%--------向上箭头
    plot([cox-xx,cox],[coy+yall-yy,coy+yall],'k')
    
    plot([cox-xall,cox-xall+xx],[coy,coy+yy],'k')%-------向左箭头
    plot([cox-xall,cox-xall+xx],[coy,coy-yy],'k')
      
    text(cox-3*xx,coy+yy*1.3,['(' num2str(cox) ',' num2str(coy) ')']);
    text(cox-xall-3*xx,coy+yy*1.3,['-x(' num2str(cox-xall) ',' num2str(coy) ')']);
    text(cox-3*xx,coy+yall+yy*1.3,['y(' num2str(cox) ',' num2str(coy+yall) ')']);
end
if right&&~up
    plot([cox-xall/2,cox+xall],[coy,coy],'k')%-----%向右
    plot([cox,cox],[coy-yall,yall/2+coy],'k')%------向下
    
    plot([cox,cox+xx],[coy-yall,coy-yall+yy],'k')%---向下箭头
    plot([cox-xx,cox],[coy-yall+yy,coy-yall],'k')
    
    plot([cox+xall-xx,xall+cox],[yy+coy,coy],'k')%--->向右箭头
    plot([cox+xall-xx,xall+cox],[coy-yy,coy],'k')
      
    text(cox,coy-yy*1.3,['(' num2str(cox) ',' num2str(coy) ')']);
    text(cox+xall,coy-yy*1.3,['x(' num2str(cox+xall) ',' num2str(coy) ')']);
    text(cox,coy-yall-yy*1.3,['-y(' num2str(cox) ',' num2str(coy-yall) ')']);
end
if ~right&&~up
    plot([cox-xall,cox+yall/2],[coy,coy],'k')%--------向左
    plot([cox,cox],[coy-yall,yall/2+coy],'k')%------向下
    
    plot([cox,cox+xx],[coy-yall,coy-yall+yy],'k')%---向下箭头
    plot([cox-xx,cox],[coy-yall+yy,coy-yall],'k')
    
    plot([cox-xall,cox-xall+xx],[coy,coy+yy],'k')%-------向左箭头
    plot([cox-xall,cox-xall+xx],[coy,coy-yy],'k')
      
    text(cox-3*xx,coy-yy*1.3,['(' num2str(cox) ',' num2str(coy) ')']);
    text(cox-xall,coy-yy*1.3,['-x(' num2str(cox-xall) ',' num2str(coy) ')']);
    text(cox-4*xx,coy-yall-yy*1.3,['-y(' num2str(cox) ',' num2str(coy-yall) ')']);
end
hold off