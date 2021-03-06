import { Validator } from "vee-validate"

// Validator.extend("password", {
//   messages: {
//     zh_CN: field => field + "不能少于十位数"
//   },
//   validate: value => {
//     return value
//   }
// })
Validator.extend("confirmPassword", {
  // messages: {
  //   zh_CN: field => "两次输入密码不一致"
  // },
  validate: (val1, val2) => {
    if (val2.lengh > 0) {
      return val1 === val2[0]
    } else {
      return false
    }
  }
})

export default Validator
