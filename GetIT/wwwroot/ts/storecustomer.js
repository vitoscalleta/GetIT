"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StoreCustomer = void 0;
var StoreCustomer = /** @class */ (function () {
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 10;
        this.ourName = firstName + lastName;
    }
    StoreCustomer.prototype.ShowName = function (name) {
        alert(name);
        return true;
    };
    return StoreCustomer;
}());
exports.StoreCustomer = StoreCustomer;
//# sourceMappingURL=storecustomer.js.map