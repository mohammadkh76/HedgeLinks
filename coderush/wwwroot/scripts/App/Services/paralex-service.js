angModule.service('parallaxService',['$http',function ($http) {
    this.createParallax=function (image, text, parentSelector) {
       var html=`
             <div class="parallax-window" data-parallax="scroll" data-image-src="${image}"></div>
                        <div class="squere parallax-middle-squere">
                       <p class="middle-squere-text">${text}</p>
             </div>`;
       
        $(parentSelector).append(html);
    }
}])