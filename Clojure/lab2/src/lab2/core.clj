(ns lab2.core)

(defn get_trapezoid_area
  [f l r]
  (* (- r l)(/ (+ (f l) (f r)) 2)))

(defn inner_memoized_integral
  []
  (let 
    [inner_integral
     (fn [mem_integral f l r h]
       (let [inner (fn [f l r h] (mem_integral mem_integral f l r h))] 
         (if
           (>= l r)
           0
           (+ 
             (get_trapezoid_area f l (+ l h))
             (inner f (+ l h) r h)))))
     mem_integral (memoize inner_integral)]
    (partial mem_integral mem_integral)))

(defn calculate_mem_integral
  [f x]
  (if (>= x 0) 
    (
      let [h 0.05 border (* h (int (/ x h)))] 
      (+ ((inner_memoized_integral) f 0 border h) 
         (get_trapezoid_area f border x))
      )
    (throw (IllegalArgumentException. "x should be > 0"))))

(calculate_mem_integral (fn [x] (* x x)) 2)
