angModule.directive('shaveDirective', function(){
    return {
        restrict: 'A',
        scope: {
            maxHeight:'=',
            shaveDirective: '=',
        },
        link:function (scope,element,attrs) {
            element[0].innerHTML=scope.shaveDirective;
            shave(element,scope.maxHeight)
            
            
        }
    };
});