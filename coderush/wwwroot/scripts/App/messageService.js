angModule.service('messageService',['$scope',function ($scope) {
    this.showMessage=function (type,message) {
        var messageDiv=`<div class="alert alert-${type}">${message}</div>`;
        
        
    }
   
}])