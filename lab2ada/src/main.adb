   with Ada.Text_IO;              use Ada.Text_IO;
   with Ada.Integer_Text_IO;      use Ada.Integer_Text_IO;
   with Ada.Numerics.Discrete_Random;

   procedure Main is

      type Index_Type is range 1 .. 100_000;
      type Int_Array is array (Index_Type) of Integer;

      Array_Size   : constant Integer := 100_000;
      Thread_Count : constant Integer := 4;

      A : Int_Array;

      -- Protected object for synchronization
      protected Global_Min is
         procedure Update (Value : Integer; Index : Index_Type);
         function Get_Min return Integer;
         function Get_Index return Index_Type;
      private
         Min_Value : Integer := Integer'Last;
         Min_Index : Index_Type := 1;
      end Global_Min;

      protected body Global_Min is
         procedure Update (Value : Integer; Index : Index_Type) is
         begin
            if Value < Min_Value then
               Min_Value := Value;
               Min_Index := Index;
            end if;
         end Update;

         function Get_Min return Integer is
         begin
            return Min_Value;
         end Get_Min;

         function Get_Index return Index_Type is
         begin
            return Min_Index;
         end Get_Index;
      end Global_Min;

      -- Task to process part of the array
      task type Min_Finder (Start_Index, End_Index : Index_Type);

      task body Min_Finder is
         Local_Min   : Integer := Integer'Last;
         Local_Index : Index_Type := Start_Index;
      begin
         for I in Start_Index .. End_Index loop
            if A(I) < Local_Min then
               Local_Min := A(I);
               Local_Index := I;
            end if;
         end loop;

         Global_Min.Update(Local_Min, Local_Index);
      end Min_Finder;

      -- Specific tasks
      Part_Size : constant Integer := Array_Size / Thread_Count;

      Finder1 : Min_Finder(Index_Type(1), Index_Type(Part_Size));
      Finder2 : Min_Finder(Index_Type(Part_Size + 1), Index_Type(2 * Part_Size));
      Finder3 : Min_Finder(Index_Type(2 * Part_Size + 1), Index_Type(3 * Part_Size));
      Finder4 : Min_Finder(Index_Type(3 * Part_Size + 1), Index_Type(Array_Size));

      package Rand_Int is new Ada.Numerics.Discrete_Random(Integer);
      Gen : Rand_Int.Generator;

   begin
      Rand_Int.Reset(Gen);

      -- Fill the array with random numbers
      for I in A'Range loop
         A(I) := Rand_Int.Random(Gen) mod 100_000;
      end loop;

      -- Insert a random negative number
      declare
         Neg_Idx : Index_Type := Index_Type(Rand_Int.Random(Gen) mod Array_Size + 1);
      begin
         A(Neg_Idx) := -(Rand_Int.Random(Gen) mod 1000);
         Put_Line("Inserted a negative number at position: " & Index_Type'Image(Neg_Idx));
      end;

      -- Tasks will finish automatically

      -- Output the result
      Put_Line("Minimum value: " & Integer'Image(Global_Min.Get_Min));
      Put_Line("Index of minimum value: " & Index_Type'Image(Global_Min.Get_Index));

   end Main;
