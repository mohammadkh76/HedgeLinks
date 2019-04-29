function datePickerFunc() {
    return {
        restrict: 'E',
        scope: {
            ngModel: '=',
            ngLabel: '@',
            minDate: '@',
            maxDate: '@',
            theme: '@',
            dateFormat: '@?',
            timeInclude: '=',
            linear: '=',
            ngDisabled: '=',
            dayText: '@',
            monthText: '@',
            yearText: '@',
            default: '@',
            maxYearRange: '=',
            showClear: '=',
            isRequired: '=',
            ngTabIndex: '@'
        },
        templateUrl: `/App/Directives/datePicker/date-picker-tmpl.html`,
        link(scope, el, attrs) {
            scope.theme = scope.theme || 'default';
            scope.dateFormat = scope.dateFormat || `DD/MM/YY${scope.timeInclude ? ' HH:mm' : ''}`;
            const minDateMoment = scope.minDate && moment(scope.minDate, scope.dateFormat);
            const maxDateMoment = scope.maxDate && moment(scope.maxDate, scope.dateFormat);
            if (minDateMoment) {
                scope.minDay = minDateMoment.date();
                // scope.minMonth = minDateMoment.month() || 0;
                scope.minMonth = minDateMoment.month();
                scope.minYear = minDateMoment.year();
            }

            if (maxDateMoment) {
                scope.maxDay = maxDateMoment.date();
                // scope.maxMonth = maxDateMoment.month() || 0;
                scope.maxMonth = maxDateMoment.month();
                scope.maxYear = maxDateMoment.year();
            }
            const currentDate = new Date();

            function setNgModel() {
                // if (!scope.ngModel) {
                //   scope.ngModel = scope.maxDate || moment().format(scope.dateFormat);
                // }
                if (scope.ngModel) {
                    const ngModelDate = moment(scope.ngModel, scope.dateFormat);
                    // require('./dat');
                    // scope.minYear = scope.minYear || 1900;
                    // scope.maxYear = scope.maxYear || currentDate.getFullYear();

                    scope.selectedDay = (ngModelDate.date()).toString();
                    // scope.selectedMonth = ngModelDate.month() || 0;
                    scope.selectedMonth = (ngModelDate.month()).toString();
                    scope.selectedYear = (ngModelDate.year()).toString();

                    if (scope.timeInclude) {
                        scope.selectedHour = (ngModelDate.hour()).toString();
                        scope.selectedMinute = (ngModelDate.minute()).toString();
                    }
                }
            }

            if (scope.default && !scope.ngModel) {
                if (scope.default === 'max') {
                    scope.ngModel = scope.maxDate;
                } else if (scope.default === 'today') {
                    scope.ngModel =  moment().format(scope.dateFormat);
                } else {
                    scope.ngModel = scope.default;
                }
            }

            const maxYearRange = scope.maxYearRange || currentDate.getFullYear();

            scope.years = new Array(maxYearRange + 1 - 1900);
            // if (ngModelDate.year() > scope.maxYear) {
            //   scope.selectedYear = scope.maxYear;
            // } else if (ngModelDate.year() < scope.minYear) {
            //   scope.selectedYear = scope.minYear;
            // } else {
            //   scope.selectedYear = ngModelDate.year();
            // }

            scope.months = [
                {
                    value: 0,
                    name: 'January',
                    nod: 31
                },
                {
                    value: 1,
                    name: 'February',
                    nod: 28
                },
                {
                    value: 2,
                    name: 'March',
                    nod: 31
                },
                {
                    value: 3,
                    name: 'April',
                    nod: 30
                },
                {
                    value: 4,
                    name: 'May',
                    nod: 31
                },
                {
                    value: 5,
                    name: 'June',
                    nod: 30
                },
                {
                    value: 6,
                    name: 'July',
                    nod: 31
                },
                {
                    value: 7,
                    name: 'August',
                    nod: 31
                },
                {
                    value: 8,
                    name: 'September',
                    nod: 30
                },
                {
                    value: 9,
                    name: 'October',
                    nod: 31
                },
                {
                    value: 10,
                    name: 'November',
                    nod: 30
                },
                {
                    value: 11,
                    name: 'December',
                    nod: 31
                }
            ];

            scope.getDays = (selectedMonth) => {
                if (selectedMonth) {
                    return new Array(scope.months[selectedMonth].nod);
                }
                return new Array(31);
            };

            scope.hours = new Array(24);
            scope.minutes = new Array(60);

            scope.setDate = () => {
                if (scope.selectedYear && Number(scope.selectedMonth) >= 0 && scope.selectedDay) {
                    let date = `${scope.selectedYear}-${Number(scope.selectedMonth) > 8 ? '' : '0'}${Number(scope.selectedMonth) + 1}-${scope.selectedDay}`;
                    let defaultFormat = 'YYYY-MM-DD';
                    if (scope.timeInclude) {
                        date += ` ${scope.selectedHour < 10 ? '0' : ''}${scope.selectedHour}:${scope.selectedMinute < 10 ? '0' : ''}${scope.selectedMinute}`
                        defaultFormat += ' HH:mm';
                    }
                    scope.ngModel = moment(date, defaultFormat).format(scope.dateFormat);
                }
            };

            scope.onYearChange = (year) => {
                if (scope.minYear === year) {
                    if (scope.selectedMonth < scope.minMonth) {
                        scope.selectedMonth = scope.minMonth;
                        if (scope.selectedDay < scope.minDay) {
                            scope.selectedDay = scope.minDay;
                        }
                    }
                }
                if (scope.maxYear === year) {
                    if (scope.selectedMonth >= scope.maxMonth) {
                        scope.selectedMonth = scope.maxMonth;
                        if (scope.selectedDay > scope.maxDay) {
                            scope.selectedDay = scope.maxDay;
                        }
                    }
                }
                scope.setDate();
            };

            scope.onMonthChange = (month) => {
                if (scope.selectedYear) {
                    if (scope.minMonth === month) {
                        if (scope.selectedDay < scope.minDay) {
                            scope.selectedDay = scope.minDay;
                        }
                    }
                    if (scope.maxMonth === month) {
                        if (scope.selectedDay > scope.maxDay) {
                            scope.selectedDay = scope.maxDay;
                        }
                    }
                }
                scope.setDate();
            };

            scope.clear = () => {
                scope.selectedYear = undefined;
                scope.selectedMonth = undefined;
                scope.selectedDay = undefined;
                scope.ngModel = undefined;
            };

            scope.$watch('ngModel', (value) => {
                if (scope.noDefault) {
                    scope.noDefault = false;
                }
                else {
                    setNgModel();
                }
            });
        }
    }
}
module.directive('datePicker', [datePickerFunc]);