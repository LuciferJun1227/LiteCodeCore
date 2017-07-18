import { USER_SIGNIN, USER_SIGNOUT } from './mutation-types'
export default {
    [USER_SIGNIN]({ commit }, user) {
        commit(USER_SIGNIN, user);
    },
    [USER_SIGNOUT]({ commit }) {
        commit(USER_SIGNOUT);
    }
}
