using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DAL
{
    public interface IWonder
    {
        #region Processor

        List<ProcessorVM> GetAllProcessors(int userid = 0, string deleteddata = null);

        IEnumerable<ProcessorVM> ProcessorPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers();

        IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id);

        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<ProcessorVM> ProcessorPrice(int min, int max, int PSize, int NPage);
        IEnumerable<ProcessorVM> ProcessorPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<ProcessorVM> ProcessorPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id);

        IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand(int min, int max, int sort);

        //IEnumerable<ProcessorVM> GetPriceDependentOnBrand(int min, int max, int sort);



        #endregion

        #region Motherboard

        List<MotherboardVM> GetAllMotherboard(int userid = 0, string deleteddata = null);

        IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers();

        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVM, int Id);

        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<MotherboardVM> MotherboardPrice(int min, int max, int PSize, int NPage);
        IEnumerable<MotherboardVM> MotherboardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<MotherboardVM> MotherboardPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id);
        IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand(int min, int max, int sort);


        #endregion

        #region HDD

        List<HddVM> GetAllHDD(int userid = 0, string deleteddata = null);

        IEnumerable<HddVM> HDDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers();

        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVM, int Id);

        IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<HddVM> HDDPrice(int min, int max, int PSize, int NPage);
        IEnumerable<HddVM> HDDPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<HddVM> HDDPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<HddVM> GetHDDDependentOnSort(int id);
        IEnumerable<HddVM> GetHDDPriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region RAM

        List<RamVM> GetAllRAM(int userid = 0, string deleteddata = null);

        IEnumerable<RamVM> RAMPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers();

        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id);

        IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<RamVM> RAMPrice(int min, int max, int PSize, int NPage);
        IEnumerable<RamVM> RAMPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<RamVM> RAMPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<RamVM> GetRAMDependentOnSort(int id);

        IEnumerable<RamVM> GetRAMPriceDependentOnBrand(int min, int max, int sort);

        #endregion

        #region SSd

        List<SsdVM> GetAllSSD(int userid = 0, string deleteddata = null);

        IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers();

        IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> SsdVMs, int Id);

        IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<SsdVM> SSDPrice(int min, int max, int PSize, int NPage);

        #endregion

        #region Graphics Card

        List<GraphicsCardVM> GetAllCard(int userid = 0, string deleteddata = null);

        IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCardBrandNamesAndNumbers();

        IEnumerable<GraphicsCardVM> GetCardProductsByPrice(IEnumerable<GraphicsCardVM> hddVM, int Id);

        IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<GraphicsCardVM> CardPrice(int min, int max, int PSize, int NPage);
        IEnumerable<GraphicsCardVM> CardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<GraphicsCardVM> CardPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<GraphicsCardVM> GetCardDependentOnSort(int id);
        IEnumerable<GraphicsCardVM> GetCardPriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region Case

        List<CaseVM> GetAllCase(int userid = 0, string deleteddata = null);

        IEnumerable<CaseVM> CasePaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCaseVMBrandNamesAndNumbers();

        IEnumerable<CaseVM> GetCaseVMProductsByPrice(IEnumerable<CaseVM> caseVMs, int Id);

        IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<CaseVM> CasePrice(int min, int max, int PSize, int NPage);
        #endregion

        #region PowerSuply

        List<PowerSupplyVM> GetAllPowerSuply(int userid = 0, string deleteddata = null);

        IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetPowerSupplyBrandNamesAndNumbers();

        IEnumerable<PowerSupplyVM> GetPowerSupplyProductsByPrice(IEnumerable<PowerSupplyVM> PowerSupplyVMs, int Id);

        IEnumerable<PowerSupplyVM> GetPowerSupplyVMsProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<PowerSupplyVM> PSPrice(int min, int max, int PSize, int NPage);
        #endregion

        #region Product Details

        CaseVM CaseDetails(string code);

        GraphicsCardVM GraphicsCardDetails(string code);

        HddVM HddDetails(string code);

        MotherboardVM MotherboardDetails(string code);

        PowerSupplyVM PowerSupplyDetails(string code);

        ProcessorVM ProcessorDetails(string code);

        RamVM RamDetails(string code);

        SsdVM SsdDetails(string code);

        #endregion

        #region comments Pagination 
        CaseVM CaseCommentsPagination(string code, int currentPageIndex);

        #endregion

        #region get Data Except one
        //List<MotherboardVM> GetTopMothers();
        List<MotherboardVM> GetMotherBoardsExceptOne(string code);

        List<ProcessorVM> GetProcessorsExceptOne(string code);

        List<RamVM> GetRamExceptOne(string code);

        List<GraphicsCardVM> GetVGAExceptOne(string code);

        List<HddVM> GetHDDExceptOne(string code);

        List<SsdVM> GetSSDExceptOne(string code);

        List<PowerSupplyVM> GetPSUExceptOne(string code);

        List<CaseVM> GetCaseExceptOne(string code);

        #endregion

        #region Get New Data
        List<MotherboardVM> GetNewMotherBoards(int userid = 0);

        List<ProcessorVM> GetNewProcessors(int userid = 0);

        List<RamVM> GetNewRam(int userid = 0);

        List<GraphicsCardVM> GetNewVGA(int userid = 0);

        List<HddVM> GetNewHDD(int userid = 0);

        List<SsdVM> GetNewSSD(int userid = 0);

        List<PowerSupplyVM> GetNewPSU(int userid = 0);

        List<CaseVM> GetNewCase(int userid = 0);

        #endregion

        #region Get Top Data

        public List<MotherboardVM> GetTopMothers(int userid = 0);

        #endregion

        #region Check Order
        string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData);

        string CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData);

        string CheckOrder(SalesVM[] OrderData);

        #endregion

        #region WishList

        List<WishListVM> GetWishList(int userid);
        string DeletefromWL(string ProductCode, int userid);
        string CheckfromWL(string ProductCode, int userid);
        string AddToWL(string ProductCode, int userid);

        #endregion

        #region Review
        public ReviewVM AddReview(ReviewVM review);
        public List<ReviewVM> Reviews(string code);
        #endregion

        #region Search

        List<Search> SearchProduct(string src);

        List<Search> SearchMotherBoard(string src);

        List<Search> SearchProcessor(string src);

        List<Search> SearchRam(string src);

        List<Search> SearchSSD(string src);

        List<Search> SearchHDD(string src);

        List<Search> SearchCase(string src);

        List<Search> SearchPowerSupply(string src);

        List<Search> SearchVGA(string src);

        #endregion

        public List<Brand> GetProductBrand();
        public List<string> GetBrandNames();

        #region Admin Project

        public List<UserVM> GetUsersData();

        public List<UserVM> GetAdmins();

        public List<SalesVM> GetProcessor();
        public List<SalesVM> GetMotherboard();
        public List<SalesVM> GetSDD();
        public List<SalesVM> GetHDD();
        public List<SalesVM> GetCases();
        public List<SalesVM> GetPowerSupplies();
        public List<SalesVM> GetGraphicsCard();
        public List<SalesVM> GetRam();











        #endregion
    }
}