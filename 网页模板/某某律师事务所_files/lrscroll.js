(function($){

	$.fn.kxbdMarquee = function(options){
		var opts = $.extend({},$.fn.kxbdMarquee.defaults, options);
		
		return this.each(function(){
			var $marquee = $(this);//婊氬姩鍏冪礌瀹瑰櫒
			var func=function(){
				var _scrollObj = $marquee.get(0);//婊氬姩鍏冪礌瀹瑰櫒DOM
				var scrollW = $marquee.width();//婊氬姩鍏冪礌瀹瑰櫒鐨勫搴�
				var scrollH = $marquee.height();//婊氬姩鍏冪礌瀹瑰櫒鐨勯珮搴�

				if(!$marquee.is(':visible')){
					setTimeout(func,100);
					return;
				}
				var $element = $marquee.children(); //婊氬姩鍏冪礌
				var $kids = $element.children();//婊氬姩瀛愬厓绱�
				var scrollSize=0;//婊氬姩鍏冪礌灏哄
				var _type = (opts.direction == 'left' || opts.direction == 'right') ? 1:0;//婊氬姩绫诲瀷锛�1宸﹀彸锛�0涓婁笅
			var end_scroll=false;

				$element.css(_type?'width':'height',10000);
				if (opts.isEqual) {
					scrollSize = $kids[_type?'outerWidth':'outerHeight']() * $kids.length;
				}else{
					$kids.each(function(){
						scrollSize += $(this)[_type?'outerWidth':'outerHeight']();
					});
				}

			if (scrollSize<(_type?scrollW:scrollH)){
					var thesize=(_type?scrollW:scrollH);
					var num=Math.ceil(thesize/scrollSize);
					scrollSize*=num;
					for(var i=0;i<num-1;i++){
						$element.append($kids.clone());
					}
					$kids = $element.children();
				}
			//bug(4432)
			$element.append($kids.clone()).css(_type?'width':'height',scrollSize*2+1);

				var numMoved = 0;
				function scrollFunc(){
					var _dir = (opts.direction == 'left' || opts.direction == 'right') ? 'scrollLeft':'scrollTop';
					if (opts.loop > 0) {
						numMoved+=opts.scrollAmount;
						if(numMoved>scrollSize*opts.loop){
							_scrollObj[_dir] = 0;
							return clearInterval(moveId);
						} 
					}
					if(opts.direction == 'left' || opts.direction == 'up'){
						var newPos = _scrollObj[_dir] + opts.scrollAmount;
						if(newPos>=scrollSize){
							newPos -= scrollSize;
						}
						_scrollObj[_dir] = newPos;
					}else{
						var newPos = _scrollObj[_dir] - opts.scrollAmount;
						if(newPos<=0){
							newPos += scrollSize;
						}
						_scrollObj[_dir] = newPos;
					}
				};

				var moveId = setInterval(scrollFunc, opts.scrollDelay);

				$marquee.hover(
					function(){
					end_scroll=true;
						clearInterval(moveId);
					},
					function(){
						clearInterval(moveId);
					end_scroll=false;
						moveId = setInterval(scrollFunc, opts.scrollDelay);
					}
				);
			try {
				var movearr = [];
				$marquee[0].addEventListener("touchstart", function(e){
//					e.preventDefault();
					var _dir = (opts.direction == 'left' || opts.direction == 'right') ? 'scrollLeft':'scrollTop';
					var pagex = e.touches[0].pageX,pagey = e.touches[0].pageY,curpos = $._parseFloat(_scrollObj[_dir]);
					end_scroll=true;
					clearInterval(moveId);
					var touchmove = function(e){
						e.preventDefault();
						if(opts.direction == 'left' || opts.direction == 'right'){
							var movex = e.touches[0].pageX - pagex,newpos = curpos - movex;
						}else if(opts.direction == 'up' || opts.direction == 'down'){
							var movey = e.touches[0].pageY - pagey,newpos = curpos - movey;
						}
						if(newpos>=scrollSize){
							newpos -= scrollSize;
						}
						if(newpos<=0){
							newpos += scrollSize;
						}
						_scrollObj[_dir] = newpos;
//						movearr.push(e.touches[0].pageX);
					},touchend = function(e){
						//e.preventDefault();
						document.removeEventListener("touchmove", touchmove, false);
						document.removeEventListener("touchend", touchend, false);


						clearInterval(moveId);
						end_scroll=false;
						setTimeout(function(){
							if(!end_scroll){ 
								clearInterval(moveId);
								moveId = setInterval(scrollFunc, opts.scrollDelay);
							}
						},1000);
						
					};
					document.addEventListener("touchmove", touchmove, false);
					document.addEventListener("touchend", touchend, false);
					document.addEventListener("touchcancel", touchend, false);
				}, false);
			} catch(exp) {
				// ...
			}	

				if(opts.controlBtn){
					$.each(opts.controlBtn, function(i,val){
						$(val).bind(opts.eventA,function(){
							opts.direction = i;
							opts.oldAmount = opts.scrollAmount;
							opts.scrollAmount = opts.newAmount;
						}).bind(opts.eventB,function(){
							opts.scrollAmount = opts.oldAmount;
						});
					});
				}
			}	
			func();
		});
	};
	$.fn.kxbdMarquee.defaults = {
		isEqual:true,
		loop: 0,
		direction: 'left',
		scrollAmount:1,
		scrollDelay:50,
		newAmount:3,
		eventA:'mousedown',
		eventB:'mouseup'
	};
	
	$.fn.kxbdMarquee.setDefaults = function(settings) {
		$.extend( $.fn.kxbdMarquee.defaults, settings );
	};
	
})(jQuery);

