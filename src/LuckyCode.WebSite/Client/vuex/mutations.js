import Vue from 'vue'
import { USER_SIGNIN, USER_SIGNOUT } from './mutation-types'

export default {
    [USER_SIGNIN](state, user) {
        sessionStorage.setItem('user', JSON.stringify(user));
        Object.assign(state, user);
        state["user"] = user;
    },
    [USER_SIGNOUT](state) {
        sessionStorage.removeItem('user');
        //  Object.keys(state).forEach(k => Vue.delete(state, k));
        delete state["user"];
    }
}