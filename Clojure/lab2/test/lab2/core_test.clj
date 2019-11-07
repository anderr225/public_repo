(ns lab2.core-test
  (:require [clojure.test :refer :all]
            [lab2.core :refer :all]))

(deftest get_trapezoid_area_test
  (testing
    (is (= 6 (get_trapezoid_area (fn [x] x) 2 4)))
    (is (= 50 (get_trapezoid_area (fn [x] x) 0 10)))))

(deftest make_integral_test
  (testing
    (is (= 333 (int ((make_integral (fn [x] (* x x))) 10))))))

(defn close_enough [v1 v2] (
  < (max (- v1 v2) (- v2 v1)) 0.3
))

(defn calc_fn [x] (/ (* x x x) 3))

(deftest compare_integral_to_calculated_fn
  (testing
    (is (close_enough (calc_fn 1) (int ((make_integral (fn [x] (* x x))) 1))))
    (is (close_enough (calc_fn 0.5) (int ((make_integral (fn [x] (* x x))) 0.5))))
    (is (close_enough (calc_fn 0.3) (int ((make_integral (fn [x] (* x x))) 0.3))))))

