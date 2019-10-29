(ns lab2.core-test
  (:require [clojure.test :refer :all]
            [lab2.core :refer :all]))

(deftest get_trapezoid_area_test
  (testing
    (is (= 6 (get_trapezoid_area (fn [x] x) 2 4)))))

(deftest get_integral_fn_test
  (testing
    (is (= 50 (int ((get_integral_fn (fn [x] x)) 10))))))

(deftest calculate_mem_integral_test
  (testing
    (is (= 2 (int (calculate_mem_integral (fn [x] x) 2))))))

(deftest inner_memoized_integral_test
  (testing
    (is (= 2 (int ((inner_memoized_integral) (fn [x] x) 0 2 0.05))))))
