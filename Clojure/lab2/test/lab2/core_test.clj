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
