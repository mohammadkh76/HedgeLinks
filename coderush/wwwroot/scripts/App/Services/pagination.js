angModule.service('paginationService', ['dataService', function (dataService) {

    this.paginate = function ({take, page, takeAll, url}) {
        return new Promise(function (resolve, reject) {
            let data = {
                take: take,
                page: page,
                takeAll: takeAll
            }
            dataService.post(url, data).then(function (response) {
                return resolve({response});
            }).catch(function (error) {
                return reject({error});
            })
        })
    }
    this.setPage=function (page) {
        
        
    }
  
    
}])